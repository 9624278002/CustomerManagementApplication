pipeline {
    agent any 
    environment {
        IMAGE_NAME = "customer-management-app"
        IMAGE_TAG = "${BUILD_NUMBER}"
        CONTAINER_NAME = "customer-management-container"
    }
    stages {
        stage('Checkout Code') {
            steps {
                git branch: 'main', url: 'https://github.com/9624278002/CustomerManagementApplication.git'
            }
        }
        stage('Build Docker Image') {
            steps {
                sh '''
                docker build -t $IMAGE_NAME:$IMAGE_TAG .
                '''
            }
        }
        stage('Stop Old Container') {
            steps {
                sh '''
                docker stop $CONTAINER_NAME || true
                docker rm $CONTAINER_NAME || true
                '''
            }
        }
        stage('Run New Container') {
            steps {
                sh '''
                docker run -d \
                --name $CONTAINER_NAME \
                -p 8080:8080 \
                $IMAGE_NAME:$IMAGE_TAG
                '''
            }
        }
        stage('Health Check') {
            steps {
                sh '''
                sleep 10
                curl -f http://localhost:80 || exit 1
                '''
            }
        }
        stage('Cleanup') {
            steps {
                sh 'docker image prune -f'
            }
        }
    }

    post {
        success {
            echo "CI/CD Pipeline Completed Successfully"
        }
        failure {
            echo "Deployment Failed - Immediate Attention Required"
        }
    }
}
