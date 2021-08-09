# Futurice Calc

This microservice project is responsible for evaluating a Base64 encoded expression.


## 1. Build & Run
* Run the following commands to start the project at the directory FuturiceCalc\FuturiceCalc:
    `dotnet build`

    `dotnet run`

This can also be done via docker. Run following commands at the root directory of the solution (FuturiceCalc).
    `docker build -t futurice-calc-app:v1 -f ./FuturiceCalc/Dockerfile .`

    `docker run -it --rm -p 5000:80 --name futurice_calc futurice-calc-app:v1`



## 2. Test
* Run the following commands to start the project at the directory FuturiceCalc\FuturiceCalc.tests :

    `dotnet test`


## 3. Deployment

This microservice is deployed on google cloud with k8s. I already have cluster (name: my-cluster) created on gcloud.
I am using google cloud shell for the deployment, but command line tools can also be used locally.

   - First, make sure that kubectl components are installed on cloud shell
      `gcloud components install kubectl`

   - Download the source code via github 
      `git clone https://github.com/psardana/FuturiceCalc.git`
      `cd FuturiceCalc`

   - build and tag your docker image
      `docker build -t eu.gcr.io/rise-foundation-event/my-images/futurice-calc-app:v1 -f ./FuturiceCalc/Dockerfile .`

   - push it docker registry created on gcloud, in our case, it's my-repos, to push the image, we have to authenticate first and then push
      `gcloud auth configure-docker europe-west1-docker.pkg.dev`
      `docker push eu.gcr.io/rise-foundation-event/my-images/futurice-calc-app:v1`

   - Now it's time to deploy using kubectl, the deployment files is in the project directors under 'k8s' folder. First we ensure that we are connected to the cluster
      `gcloud container clusters get-credentials my-cluster --zone europe-west1-b`

   - Navigate to k8s folder with `cd k8s` and run following commands
      `kubectl create namespace futurice` this will create a namsapce
      `kubectl apply -f deployment.yaml`  this will create a deployment
      `kubectl apply -f service.yaml`  this will expose the deployment inside the cluster


Deployment is now finished and application can be access over
http://35.195.183.178/index.html


## 4. Swagger

http://35.195.183.178/index.html

## 5. Version

Application uses a version header, if you pass a header 'x-api-version' with the value 2.0, it will use the implementation of having the expression broken down in a list and then evaluating it, in default case (1.0), it uses ncalc library.

## 6. Load Test

Load test is done with the use of Locust. Report is also in the folder. To run it again, make sure you have python, pip3 and locust. Following commands would be needed - `python`and `pip3` are installed locally, f.e.: `sudo apt install python3-pip`
- `locust` is installed: `pip3 install locust`

Then run `locust -f LoadTest\FuturiceCalcLoadTest.py', provide users, spawn rate and host name





