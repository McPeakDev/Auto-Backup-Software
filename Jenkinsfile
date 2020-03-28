pipeline {
  agent {
    docker {
      image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
    }

  }
  environment {
   HOME = '/tmp'
  } 
  stages {
    stage('Build App') {
      steps {
        sh '''cd GSMAutoBackup/ 
dotnet publish -c Release -r linux-x64'''
      }
    }

  }
}
