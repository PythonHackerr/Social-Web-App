# Kakaw (aka SoialApp)

A highly discouraged solution for all your instant messaging needs.

## Build and push to docker hub

```sh
docker context use default
docker compose build
docker compose push
```

Alterantively

```sh
./build-push.bat
```

## Deploy to Azure ACI

### Create Docker context for Azure ACI

https://docs.docker.com/cloud/aci-integration/

### Deploy a single container

```sh
docker --context <aci-context> run -p 80:80 <user>/<image>:latest
```

### Deploy with docker compose

```sh
docker --context <aci-context> compose up
```

Alternatively

```sh
./deploy-aci.bat <aci-context>
```

## Build with Buildah and push to Docker Hub

Login (required to push)
```sh
buildah login docker.io
```

Build (example for UserProfileService)

```sh
git clone https://github.com/thejoun/pis-project.git
cd pis-project/
buildah bud -f UserProfileService/Dockerfile -t thejoun/user-profile-service .
```

Push (example for UserProfileService)
```sh
buildah push thejoun/user-profile-service:latest
```