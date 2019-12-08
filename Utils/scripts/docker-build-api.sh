#!/bin/bash
#docker build -t [image name:tag] . -f ./[path_to_dockerfile]
#command to run in shell => e.g. 
# DOCKER_TAG_BRANCH='master' APP_NAME='weather' Utils/scripts/docker-build-api.sh


DOCKER_TAG_BRANCH=${DOCKER_TAG_BRANCH:-develop}
APP_NAME=${APP_NAME:-general}


if [ "$APP_NAME" == "general" ]
then
	echo 'tu w gen'
    APP_DIR="City-Informer.City.General.Api"
elif [ "$APP_NAME" == "weather" ]
then
	echo 'mamy w weather cond'
    APP_DIR='City-Informer.City.Weather.Api'
fi

docker build -t pablokielek/city-$APP_NAME-api:$DOCKER_TAG_BRANCH . -f ./$APP_DIR/Dockerfile

