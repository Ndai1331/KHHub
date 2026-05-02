# Local Kubernetes Guide

This guide explains how to deploy your microservice template into your local Kubernetes cluster.

## Pre-requirements

* [Docker for Desktop](https://www.docker.com/products/docker-desktop/) with Kubernetes enabled
* [Helm](https://helm.sh/docs/intro/install/) for running helm charts
* Install NGINX ingress using helm:
```powershell
helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
helm repo update
helm upgrade --install --version=4.0.19 ingress-nginx ingress-nginx/ingress-nginx --set controller.config.enable-underscores-in-headers="true"
```
## Configuring HTTPS for Local K8s

You can run the solution on staging environment in your local Kubernetes cluster with HTTPS. There are various ways to create a self-signed certificate.

### Installing mkcert
This guide will be using `mkcert` for creating self-signed certificates. Follow the [installation guide](https://github.com/FiloSottile/mkcert#installation) to install mkcert.

### Creating mkcert Root CA
Use the command to create root (local) certificate authority for your certificates:
```powershell
mkcert -install
```

> **Note:** All the certificates created by mkcert certificate creation will be trusted by the local machine
### Run mkcert

Use the `create-tls-secrets.ps1` PowerShell script to create self-signed certificates for your domains and the tls secret for Kubernetes.

## Building Images

Run `build-all-images.ps1` to build all Docker images for the solution. Do this whenever you change the solution code. If you only change a single project, you can use `build-image.ps1` to build only that image, for a faster build. See `build-all-images.ps1` file's content to learn how to use `build-image.ps1` for a single project.

> Note that you can also use ABP Studio to build one or all the images.

### Build and push all images to Docker Hub (`khub`)

To build every service once and push rolling tags (`<service>-latest`) to your registry in a single run, use `build-all-khub-images.ps1` from this folder.

1. Sign in to Docker Hub (or your registry): `docker login`
2. Run from the repo root (or `cd` into `etc/helm` first).

**Windows (PowerShell):**

```powershell
cd etc/helm
./build-all-khub-images.ps1
```

**macOS / Linux (zsh, bash):** do not run `./build-all-khub-images.ps1` by itself — the file is not a shell script and usually has no execute bit, so you get `permission denied`. Invoke PowerShell Core instead:

```bash
cd etc/helm
pwsh -File ./build-all-khub-images.ps1
```

Optional: after `chmod +x build-all-khub-images.ps1`, `./build-all-khub-images.ps1` works if `pwsh` is on your `PATH` (the script includes a `pwsh` shebang).

**Target OS/CPU (Linux AMD64):** by default the script uses `-Platform "linux/amd64"`. That passes `--platform linux/amd64` to `docker build` and runs `dotnet publish -r linux-x64` into `bin/Release/net10.0/publish` so binaries match x64 Linux servers when you build on another architecture (e.g. Apple Silicon). For Linux arm64 hosts, use `-Platform "linux/arm64"`. To build for the runner’s native arch only (e.g. local testing), pass an empty platform:

```powershell
pwsh -File ./build-all-khub-images.ps1 -Platform ""
```

You can also pass `-Platform` when calling `build-image.ps1` for a single project.

By default images are pushed to `longnguyen1331/khub` (e.g. `longnguyen1331/khub:web-latest`). Override the registry if needed:

```powershell
pwsh -File ./build-all-khub-images.ps1 -Registry "youruser/yourrepo"
```

Optional: set a fixed local build tag for `build-image.ps1` / `values.localdev.yaml` (remote tags stay `<service>-latest` only):

```powershell
pwsh -File ./build-all-khub-images.ps1 -Version "20260101.120000"
```

### GitHub Actions — build and push on push

Workflow file: [`.github/workflows/docker-hub-khub.yml`](../../.github/workflows/docker-hub-khub.yml).

It runs on every `push` to `main`, `master`, or `develop`. **What gets built depends on the commit message** (first `[...]` block anywhere in the message):

| Commit message pattern | Behaviour |
| --- | --- |
| `[All] ...` or `[all] ...` | Build and push **all** images (`-All`). |
| `[services/masterdata, apps/web] ...` | Build and push **only** images for those repo path segments (comma-separated; spaces ignored). Unknown paths fail the job. |
| No `[...]` in the message | Workflow succeeds; **no** Docker build/push (saves CI time). |

**Manual run** (Actions → Run workflow): always builds **all** images.

**Allowed path segments** (must match the repo tree; use lowercase as below):

| Segment | Image tag suffix |
| --- | --- |
| `services/administration` | `administration-latest` |
| `services/identity` | `identity-latest` |
| `services/audit-logging` | `auditlogging-latest` |
| `services/gdpr` | `gdpr-latest` |
| `services/ai-management` | `aimanagement-latest` |
| `services/language` | `language-latest` |
| `services/masterdata` | `masterdata-latest` |
| `gateways/web` | `webgateway-latest` |
| `gateways/mobile` | `mobilegateway-latest` |
| `apps/auth-server` | `authserver-latest` |
| `apps/web` | `web-latest` |

Example:

```text
[services/masterdata, apps/web] fix report export
```

**Repository secrets** (Settings → Secrets and variables → Actions → Secrets):

| Name | Required | Description |
| --- | --- | --- |
| `DOCKERHUB_USERNAME` | Yes | Docker Hub user name |
| `DOCKERHUB_TOKEN` | Yes | [Access token](https://hub.docker.com/settings/security) (recommended; do not use password if token is available) |
| `DOCKER_IMAGE_REGISTRY` | No | Image prefix, e.g. `longnguyen1331/khub`. If omitted, the workflow defaults to `longnguyen1331/khub`. |

Locally you can mirror CI: build **all** with `-All`, or filter with `-PathsSpec`:

```powershell
pwsh -File ./build-all-khub-images.ps1 -PathsSpec "services/masterdata,apps/web"
pwsh -File ./build-all-khub-images.ps1 -All
```

## Install Charts

Run `install.ps1` to install or upgrade the helm charts to the Kubernetes cluster.

> Note that you can also use ABP Studio to install/uninstall charts.

## Add Entries to the Hosts File

Add the lines below to your hosts file:

- **Windows**: `C:\Windows\System32\drivers\etc\hosts`
- **macOS / Linux**: `/etc/hosts` (edit with sudo, e.g. `sudo nano /etc/hosts`)

```
127.0.0.1 khhub-local-web
127.0.0.1 khhub-local-webgateway
127.0.0.1 khhub-local-authserver
```

> Note that ABP Studio automatically adds these entries to your `hosts` file when you *Connect* to your Kubernetes cluster using ABP Studio's Kubernetes integration.

## Browse

Now, you can browse the URL: `https://khhub-local-web`

## Uninstall Charts

Run `uninstall.ps1` to uninstall the helm charts from the Kubernetes cluster.

