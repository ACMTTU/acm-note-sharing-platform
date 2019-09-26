# Secrets Service

This service is used for secret management. It is a wrapper of
Azure Key Vault. Using this service, any application or service
within the cluster is able to grab connection strings in a secure
way.

# Where is this deployed?

This is deployed in the `secrets` namespace within AKS

# How do we connect to it?

You can reach this service by making an http call to...

```http://secretsservice.secrets.svc.cluster.local/api/connection/<name of connection string you want>```

