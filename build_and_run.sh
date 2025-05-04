#!/usr/bin/env bash
set -e

IMAGE_NAME="testmvcjenkinswithunit"
CONTAINER_NAME="container-testmvcjenkinswithunit"

echo "Building Docker image..."
docker build -t ${IMAGE_NAME}:latest .

echo "Stopping any existing container..."
docker rm -f ${CONTAINER_NAME} 2>/dev/null || true

echo "Starting new Jenkins container..."
docker run -d \
  --name ${CONTAINER_NAME} \
  -p 8080:8080 \
  -p 50000:50000 \
  -v jenkins_data:/var/jenkins_home \
  -v /var/run/docker.sock:/var/run/docker.sock \
  ${IMAGE_NAME}:latest

echo "Container '${CONTAINER_NAME}' is starting up..."
echo "Check logs with: docker logs -f ${CONTAINER_NAME}"
echo "Once ready, access Jenkins at: http://localhost:8080"

# Show how to retrieve the initial admin password:
echo "Initial admin password (once container is up) can be read via:"
echo "  docker exec ${CONTAINER_NAME} cat /var/jenkins_home/secrets/initialAdminPassword"
