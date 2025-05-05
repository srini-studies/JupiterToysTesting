pipeline {
    agent any

    environment {
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = 'true'
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
    }

    stages {
        stage('Clone Repository') {
            steps {
                git url: 'https://github.com/srini-studies/JupiterToysTesting.git', branch: 'main'
            }
        }

        stage('Install .Net dependencies') {
            steps {
                // Install .NET dependencies
                bat 'dotnet restore'               
            }
        }

        stage('Build Project') {
            steps {
                // bat 'dotnet build --configuration Release'
                bat 'dotnet build --configuration Release || exit /b %errorlevel%'
            }
        }

        stage('Install Playwright') {
            steps {
                // Install Playwright CLI and browsers
                bat 'dotnet tool install --global Microsoft.Playwright.CLI || true'
                bat 'dotnet --info'
                bat '%USERPROFILE%\\.dotnet\\tools\\playwright.exe install'                
            }        
        }

        stage('Run Tests') {
            steps {
                bat 'dotnet test --logger:trx'
            }
        }

        stage('Publish Results') {
            steps {
                mstest testResultsFile: '**/TestResults/*.trx'
			}
		}

    }
    
    post {
        always {
            echo 'Pipeline completed'
        }
    }
}
