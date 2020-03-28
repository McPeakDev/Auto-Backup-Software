pipeline {
  agent {
    docker {
      image 'mcr.microsoft.com/dotnet/core/sdk:3.1	'
    }

  }
  stages {
    stage('Build App') {
      steps {
        sh '''echo \'Building API ...\'
sh \'cd BikeShopAnalyticsAPI/ && dotnet publish -c Release -r linux-x64 --self-contained false\'
'''
      }
    }

  }
}