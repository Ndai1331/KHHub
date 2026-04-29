param (
    $Namespace="khhub-local",
    $User = ""
)

$BaseNamespace = $Namespace
if([string]::IsNullOrEmpty($User) -eq $false)
{
    $Namespace += '-' + $User
}

mkcert --cert-file "${Namespace}.pem" --key-file "${Namespace}-key.pem" "${Namespace}" "${Namespace}-authserver" "${Namespace}-web"  "${Namespace}-webgateway" "${Namespace}-kibana" "${Namespace}-grafana" "${Namespace}-prometheus" "${Namespace}-mobilegateway"
kubectl create namespace ${Namespace}
kubectl create secret tls -n ${Namespace} ${BaseNamespace}-tls --cert=./${Namespace}.pem --key=./${Namespace}-key.pem