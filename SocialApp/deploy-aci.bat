set acicontext=%1

docker --context %acicontext% compose -f docker-compose.aci.yml up