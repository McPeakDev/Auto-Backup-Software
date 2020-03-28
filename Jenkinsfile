pipeline {
  agent {
    docker {
      image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
    }

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