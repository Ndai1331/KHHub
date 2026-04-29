# KHHub

## About this solution

This is a startup template to create microservice based solutions. All the fundamental ABP modules are already installed and configured. Check the [Microservice Solution Template](https://abp.io/docs/latest/solution-templates/microservice) documentation for more info.

### Pre-requirements

* [.NET10.0+ SDK](https://dotnet.microsoft.com/download/dotnet)
* [Node v18 or 20](https://nodejs.org/en)
* [Docker](https://www.docker.com/) for running infrastructure dependencies
* [Redis](https://redis.io/) for distributed caching

### Solution structure

This is a microservice solution that consists of the following components:

#### Applications (`apps/`)

* `auth-server`: Authentication server using OpenIddict for OAuth 2.0 / OpenID Connect.
* `web`: ASP.NET Core MVC / Razor Pages web application.

#### Gateways (`gateways/`)

The solution uses the Backend for Frontend (BFF) pattern with dedicated gateways:

* `mobile`: Mobile gateway for mobile applications.
* `web`: Web gateway for the main web application.

#### Services (`services/`)

* `administration`: Permissions, settings, and features management service.
* `audit-logging`: Audit logging service for tracking system activities.
* `gdpr`: GDPR compliance service for data protection.
* `identity`: User and role management service.
* `text-template-management`: Text template management service.

#### .NET Aspire (`aspire/`)

This solution includes .NET Aspire orchestration for local development and observability. The Aspire project provides:

* Service discovery and configuration
* Distributed tracing and logging
* Health checks and metrics
* Local development orchestration

To run the solution with Aspire, navigate to the `aspire` folder and run:

```bash
dotnet run
```

## Before Running the Solution

### Generate Signing-Certificate for AuthServer 

#### Installing mkcert
This guide will be using `mkcert` for creating self-signed certificates. If it is not installed in your system, follow the [installation guide](https://github.com/FiloSottile/mkcert#installation) to install mkcert.

Then use the command to create root (local) certificate authority for your certificates:
```powershell
mkcert -install
```

#### Generate Signing-Certificate

Navigate to `/apps/auth-server/KHHub.AuthServer` folder and run:

```bash
dotnet dev-certs https -v -ep ./openiddict.pfx -p d294fd54-ace5-43b2-8cae-fb7d0c246e95
```

to generate pfx file for signing tokens by AuthServer.

> This should be done by every developer.

### Install Client-Side Libraries

Run the following command in this folder:

````bash
abp install-libs
````


### Running the React Native Application

To run the React Native application, you should first run the `MobileGateway` and the `AuthService` so the mobile application can communicate with the backend services.

Before running the React Native application, install the dependencies by running `yarn install` or `npm install` in the `apps/mobile/react-native` directory.

> For further information, please refer to [Getting Started with the React Native](https://abp.io/docs/latest/framework/ui/react-native?Architecture=Tiered) documentation.

#### Web View

The quickest way to test the application is using the web view. While testing on a physical device is also supported, we recommend using [local HTTPS development](https://docs.expo.dev/guides/local-https-development/) as it requires fewer backend modifications.

Follow these steps to set up the web view:

1. Navigate to the `apps/mobile/react-native` directory and start the application by running:
   ```bash
   yarn web
   ```

2. Generate SSL certificates by running the following command in a separate directory:
   ```bash
   mkcert localhost
   ```

3. Set up the local proxy by running:
   ```bash
   yarn create:local-proxy
   ```
   The default port is `443`. To use a different port, specify the `SOURCE_PORT` environment variable:
   ```bash
   SOURCE_PORT=8443 yarn create:local-proxy
   ```

4. If you changed the port in the previous step, update the `apiUrl` in `Environment.ts` accordingly.

5. Update the mobile application settings in the database and re-run the migrations. If you specified a custom port, ensure the port is updated in the configuration as well:
   ```json
   "OpenIddict": {
     "Applications": {
       "KHHub_Mobile": {
         "ClientId": "KHHub_Mobile",
         "RootUrl": "https://localhost"
       }
     }
   }
   ```

#### Android

If you are running the application on an Android emulator or device, set up port mapping using the `adb` tool so the app can reach backend services on your development machine:

```bash
adb reverse tcp:44369 tcp:44369 # port mapping for mobile gateway
adb reverse tcp:44315 tcp:44315 # port mapping for auth service
```

> You should set up port mappings for both `MobileGateway` and `AuthService` services as described above.


### Running the React Native Mobile Application

To run the React Native mobile application, you should first run the `MobileGateway` and the `AuthServer` so that the React Native app can communicate with the backend services.

#### Pre-requirements

* [Node.js v18 or later](https://nodejs.org/)
* [Expo CLI](https://docs.expo.dev/get-started/installation/)
* For iOS: macOS with Xcode installed
* For Android: Android Studio with an emulator or a physical device

#### Configuration

Before running the application, you need to configure the backend URLs in the `apps/mobile/Environment.ts` file. Update the IP addresses with your local machine's IP address:

```typescript
const yourIP = 'Your Local IP Address e.g. 192.168.1.64';
```

> Your mobile device/emulator must be able to reach this IP address on your network.

#### Running the Application

Navigate to the `apps/mobile` folder and run:

```bash
npm install
npm start
```

Then press `a` for Android or `i` for iOS in the terminal to launch the app on the respective platform.

> For further information, please refer to [Getting Started with React Native](https://abp.io/docs/latest/framework/ui/react-native) documentation.


### Running on a Kubernetes Cluster Environment

To run the application(s) on your Kubernetes cluster environment, follow these steps:

- Navigate to the [/etc/helm](./etc/helm) directory within your terminal or command prompt.
- Execute the [create-tls-secrets.ps1](./etc/helm/create-tls-secrets.ps1) PowerShell command.
- Open the Kubernetes menu in ABP Studio, then within the Helm tab:
  - Build Docker Images: In the `Charts` tree context menu, click on the `Build Docker Image(s)` option. This will start the process of building your Docker images.
  - Install Charts: After the Docker images have been built, you can install your Helm charts. To do this, go to `Charts->Commands` in the context menu and click on `Install Chart(s)`.

Also, make sure to review the [pre-requirements](./etc/helm/README.md#Pre-requirements) before proceeding.

> This should be done by every developer.

### Deploying the application

Deploying an ABP application follows the same process as deploying any .NET or ASP.NET Core application. However, there are important considerations to keep in mind. For detailed guidance, refer to ABP's [deployment documentation](https://abp.io/docs/latest/deployment/distributed-microservice).

### Additional resources

#### Internal Resources

You can find detailed setup and configuration guide(s) for your solution below:

* [Docker-Compose for Infrastructure Dependencies](./etc/docker/README.md)
* [Local Kubernetes Guide](./etc/helm/README.md)

#### External Resources

You can see the following resources to learn more about your solution and the ABP Framework:

* [Microservice Development Tutorial](https://abp.io/docs/latest/tutorials/microservice)
* [Microservice Solution Template](https://abp.io/docs/latest/solution-templates/microservice)
