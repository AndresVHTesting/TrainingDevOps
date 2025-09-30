# TrainingDevOps

## Steps to Build the Image, Run the Container, and Manage It

1. **Go to the directory where the Dockerfile is located:**
    ```bash
    cd TrainingDevOps
    ```

2. **Build the Docker image:**
    ```bash
    docker build -t trainingdevops:latest .
    ```

3. **Run the container:**
    ```bash
    docker run -d --name trainingdevops-app -p 8080:8080 -p 8081:8081 trainingdevops:latest
    ```

4. **Open Swagger UI in your browser:**
    ```
    http://localhost:8080/swagger/index.html
    ```

---

### Optional Container Management Commands

5. **Stop the running container:**
    ```bash
    docker stop trainingdevops-app
    ```

6. **(Re)start the container:**
    ```bash
    docker start trainingdevops-app
    ```

7. **Remove the container:**
    ```bash
    docker rm trainingdevops-app
    ```

8. **Remove the image:**
    ```bash
    docker rmi trainingdevops
    ```
---

**Note:**  
- Step 6 removes the container, so you need to run step 3 again to create a new container.  
- Use step 7 to start a stopped container without recreating it.

---

## Build, Run, and Test the ASP.NET Web API Project Using the Command Line

1. **Clean the solution:**
    ```bash
    dotnet clean
    ```

2. **Restore dependencies:**
    ```bash
    dotnet restore
    ```

3. **Restore dependencies:**
    ```bash
    dotnet build
    ```

4. **Run tests and generate TRX test results:**
    ```bash
    dotnet test TrainingDevOpsTests/TrainingDevOpsTests.csproj --logger trx --results-directory ./TestResults
    ```

5. **List all available tests:**
    ```bash
    dotnet test TrainingDevOpsTests/TrainingDevOpsTests.csproj --list-tests
    ```