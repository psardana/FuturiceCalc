from locust import task, constant
from locust.contrib.fasthttp import FastHttpUser

import json
import random
import gevent
import csv

class FuturiceCalcLoadTest(FastHttpUser):

    wait_time = constant(1)

    @task()
    def runTest(self):
        gevent.spawn(self.evaluateExp)

    def evaluateExp(self):
        self.client.get('http://35.195.183.178/api/evaluate-expression/MiAqICgyMy8oMyozKSktIDIzICogKDIqMyk=')
