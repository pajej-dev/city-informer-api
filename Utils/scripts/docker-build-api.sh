#!/bin/bash
#docker build -t [image name:tag] . -f ./[path_to_dockerfile]
#command to run in shell (from the root of the repo)  => 
# DOCKER_TAG_BRANCH='master' APP_NAME='weather' Utils/scripts/docker-build-api.sh


DOCKER_TAG_BRANCH=${DOCKER_TAG_BRANCH:-develop}
APP_NAME=${APP_NAME:-general}


if [ "$APP_NAME" == "general" ]
then
    APP_DIR="City-Informer.City.General.Api"
elif [ "$APP_NAME" == "weather" ]
then
    APP_DIR='City-Informer.City.Weather.Api'
fi

docker build -t pablokielek/city-$APP_NAME-api:$DOCKER_TAG_BRANCH . -f ./$APP_DIR/Dockerfile

