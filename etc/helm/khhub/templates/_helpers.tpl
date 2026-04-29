{{- define "khhub.hosts.authserver" -}}
{{- print "https://" (.Values.global.hosts.authserver | replace "[RELEASE_NAME]" .Release.Name) -}}
{{- end -}}
{{- define "khhub.hosts.webgateway" -}}
{{- print "https://" (.Values.global.hosts.webgateway | replace "[RELEASE_NAME]" .Release.Name) -}}
{{- end -}}
{{- define "khhub.hosts.kibana" -}}
{{- print "https://" (.Values.global.hosts.kibana | replace "[RELEASE_NAME]" .Release.Name) -}}
{{- end -}}
{{- define "khhub.hosts.prometheus" -}}
{{- print "https://" (.Values.global.hosts.prometheus | replace "[RELEASE_NAME]" .Release.Name) -}}
{{- end -}}
{{- define "khhub.hosts.grafana" -}}
{{- print "https://" (.Values.global.hosts.grafana | replace "[RELEASE_NAME]" .Release.Name) -}}
{{- end -}}
{{- define "khhub.hosts.web" -}}
{{- print "https://" (.Values.global.hosts.web | replace "[RELEASE_NAME]" .Release.Name) -}}
{{- end -}}
{{- define "khhub.hosts.mobilegateway" -}}
{{- print "https://" (.Values.global.hosts.mobilegateway | replace "[RELEASE_NAME]" .Release.Name) -}}
{{- end -}}
