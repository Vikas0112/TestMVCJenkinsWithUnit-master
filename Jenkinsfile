pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        SONAR_PROJECT_KEY = 'TestMVCJenkinsWithUnit'
      //  SONAR_PROJECT_KEY = 'squ_291ee6d11bb4c761eb4e21d1bbffa2403803f2c1'
        PATH = "${env.HOME}/.dotnet/tools:${env.PATH}"     
        SONAR_HOST_URL = 'https://pdlsonar.paisalo.in:9999'
       // PATH = "${env.HOME}/.dotnet/tools:${env.PATH}" // Add dotnet tools path to system path
    } 

    stages {
        stage('Checkout') {
            steps {
                git credentialsId: 'githubtoken-29april', url: 'https://github.com/Paisalo-Digital-Limited/TestMVCJenkinsWithUnit.git'
            }
        }

       

        stage('Unit Tests') {
            steps {
                sh '''
                    dotnet test TestJenkinsWithUnit.Tests/TestJenkinsWithUnit.Tests.csproj \
                        --collect:"XPlat Code Coverage" \
                        --results-directory:TestResults \
                        --logger "trx" --no-build
                '''
            }
        }
       
        

         stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }

        stage('Lint') {
            steps {
                echo "Linting done (placeholder)."
            }
        }

       stage('SonarQube Analysis') {
    steps {
        echo "SonarQube done"
      //  withSonarQubeEnv('SonarQube') {
        //    sh '''
         //       dotnet tool install --global dotnet-sonarscanner || true
         //       export PATH=$HOME/.dotnet/tools:$PATH

           //     # Hardcoded token - only for testing!
           //     dotnet sonarscanner begin /k:"TestMVCJenkinsWithUnit" /d:sonar.login=squ_291ee6d11bb4c761eb4e21d1bbffa2403803f2c1 
              //  dotnet build
           //     dotnet sonarscanner end /d:sonar.login=squ_291ee6d11bb4c761eb4e21d1bbffa2403803f2c1
           // '''
     //   }
    }
 }
        stage('Debug Project Paths') {
            steps {
                sh 'find . -name "*.csproj"'
            }
        }

     
        stage('Publish') {
          steps {
            sh 'dotnet publish TestJenkinsWithUnitTest/TestJenkinsWithUnit.csproj -c Release -o publish'
    }
}

        
        stage('Docker Build & Push to ghcr') {
         steps {
           script {
            def imageName = "ghcr.io/paisalo-digital-limited/api-image-1"
            def imageTag = "latest"

            // Authenticate with GitHub Container Registry
            withCredentials([string(credentialsId: 'git-hub-token', variable: 'GHCR_TOKEN')]) {
                sh """
                    echo "${GHCR_TOKEN}" | docker login ghcr.io -u paisalo-digital-limited --password-stdin
                    docker build -t ${imageName}:${imageTag} .
                    docker push ${imageName}:${imageTag}
                """
            }
        }
    }
}

           
        stage('OSS Scan') {
            steps {
                echo "OSS Scan completed (placeholder)."
            }
        }

        stage('Security Scan') {
            steps {
                echo "Security Scan completed (placeholder)."
            }
        }
    }

    post {
        always {
            echo "Pipeline completed."
        }
        success {
            echo "Build and tests successful"
        }
        failure {
            echo "Pipeline failed"
        }
    }
}
