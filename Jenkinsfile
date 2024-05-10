properties([pipelineTriggers([githubPush()])])

pipeline {  
    agent any
    when { 
        changeset pattern: ".*UserProfileService.*", comparator: "REGEXP"
    }
    environment {
        PARENT_DIR = './SocialApp/'
        PROJECT_NAME = 'UserProfileService'
        IMAGE = 'thejoun/user-profile-service'
        IMAGE_TAG = 'latest'
        GITHUB_CREDENTIALS = 'github-token'
        DOCKER_CREDENTIALS = 'docker-io'
        REPO_URL = 'https://github.com/thejoun/pis-project.git'
        BRANCH = 'main'
    }
    stages {  
        stage('Checkout') {  
            steps {
                git credentialsId: "${GITHUB_CREDENTIALS}", url: "${REPO_URL}", branch: "${BRANCH}"
                sh 'pwd'
                sh 'ls'
            }  
        }  
        stage('Test') {  
            steps {  
                sh "dotnet test ${PARENT_DIR}${PROJECT_NAME}/ /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura"
                // re-enable this when there are any tests to run
                // cobertura coberturaReportFile: '**/coverage.cobertura.xml'
            }  
        }
        stage('Build') {  
            steps { 
                dir("${PARENT_DIR}"){
                    sh "buildah bud -f ${PROJECT_NAME}/Dockerfile -t ${IMAGE} ."
                }
            }  
        }
        stage('Push') {  
            steps { 
                withCredentials([usernamePassword(credentialsId: "${DOCKER_CREDENTIALS}", passwordVariable: 'PASSWORD', usernameVariable: 'USER')]){
                    sh 'buildah login -u ${USER} -p ${PASSWORD} docker.io'
                }
                
                sh "buildah push ${IMAGE}:${IMAGE_TAG}"
            }  
        }
        
        // old stuff, not needed with the container pipeline

        // publishing .net project and submitting an artifact to jenkins
        // stage('Publish') {
        //     steps {
        //         sh 'dotnet publish ${PROJECT_NAME} --configuration RELEASE'
        //         zip zipFile: '${PROJECT_NAME}.zip', archive: false, dir: '${PROJECT_NAME}\\${PROJECT_NAME}\\bin\\Release\\net7.0\\publish', overwrite: true
        //         archiveArtifacts artifacts: '${PROJECT_NAME}.zip', fingerprint: true
        //     }
        // }

        // packing the project and pushing to nuget
        // stage('Pack') {
        //     steps {
        //         sh 'dotnet pack %WORKSPACE%\\Kakaw\\Kakaw\\Kakaw.csproj --configuration RELEASE'
        //         sh 'dotnet nuget push "**/*.nupkg" -k oy2e5oudfcpcfjs7ufjabtcf6fyqdc6u7b7y6ozivglhle -s https://api.nuget.org/v3/index.json --skip-duplicate'
        //     }
        // }
    }
}