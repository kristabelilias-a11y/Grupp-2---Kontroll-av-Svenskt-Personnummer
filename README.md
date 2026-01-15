# Swedish Personnummer Validator

A . NET console application that validates Swedish personal identity numbers (personnummer) using the Luhn algorithm and date validation. 

## Table of Contents
- [About Swedish Personnummer](#about-swedish-personnummer)
- [How the Validation Works](#how-the-validation-works)
- [Prerequisites](#prerequisites)
- [Running Locally](#running-locally)
- [Running Tests](#running-tests)
- [Docker](#docker)
- [CI/CD Pipeline](#cicd-pipeline)

---

## About Swedish Personnummer

A Swedish personnummer (personal identity number) is a 10 or 12-digit number assigned to individuals registered in Sweden. It follows the format: 

```
YYMMDD-XXXX  (10 digits with separator)
YYYYMMDD-XXXX  (12 digits with separator)
```

### Structure: 
- **YYMMDD / YYYYMMDD**: Date of birth (year, month, day)
- **-** or **+**: Separator (+ used for people over 100 years old)
- **XXX**:  Birth number (odd for males, even for females)
- **C**: Check digit (calculated using the Luhn algorithm)

### Rules:
1. **Date must be valid**: The birth date must be a real calendar date
2. **Luhn algorithm**: The check digit must pass Luhn validation
3. **Format**: Must follow YYMMDD-XXXX or YYYYMMDD-XXXX format

---

## How the Validation Works

This application validates personnummer using a two-step process:

### 1. Date Validation
- Extracts the date portion (YYMMDD or YYYYMMDD)
- Verifies it's a valid calendar date
- Handles century detection for 10-digit format

### 2. Luhn Algorithm Validation
The Luhn algorithm (also known as the "modulus 10" algorithm) works as follows:

1. Take the first 9 digits of the personnummer (excluding the check digit)
2. Starting from the right, double every second digit
3. If doubling results in a two-digit number, sum the digits (e.g., 14 → 1+4=5)
4. Sum all the resulting digits
5. The check digit is the number needed to make the total sum divisible by 10

**Example**: `811218-9876`
```
Digits: 8 1 1 2 1 8 9 8 7 [6]
Double: 16 1 2 2 2 8 18 8 14
Sum:    7+1+2+2+2+8+9+8+5 = 44
Check:  (50 - 44) = 6 ✓ Valid! 
```

---

## Prerequisites

To run this application locally, you need:

- **. NET SDK 9.0** or later
  - Download:  https://dotnet.microsoft.com/download

To verify your installation:
```bash
dotnet --version
```

---

## Running Locally

### 1. Clone the Repository
```bash
git clone https://github.com/kristabelilias-a11y/Grupp-2---Kontroll-av-Svenskt-Personnummer.git
cd Grupp-2---Kontroll-av-Svenskt-Personnummer
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Run the Application
```bash
cd Personnummer-App
dotnet run
```

### 4. Use the Application
The application will prompt you to enter a personnummer: 

```
=== Swedish Personnummer Validator ===
Enter a Swedish personnummer (format: YYMMDD-XXXX or YYYYMMDD-XXXX):
> 811218-9876

✓ Valid personnummer! 

Check another?  (y/n):
```

---

## Running Tests

This project includes unit tests to verify the validation logic. 

### Run All Tests
```bash
dotnet test
```

### Run Tests with Detailed Output
```bash
dotnet test --verbosity detailed
```

### Run Tests with Coverage (if configured)
```bash
dotnet test /p:CollectCoverage=true
```

---

## Docker

The application is containerized and can be run using Docker.

### Prerequisites
- **Docker** installed on your machine
  - Download: https://www.docker.com/get-started

### Option 1: Pull and Run from DockerHub

Once the DockerHub secrets are configured and the image is published:

```bash
# Pull the image
docker pull <dockerhub-username>/personnummer-validator:latest

# Run the container interactively
docker run -it <dockerhub-username>/personnummer-validator:latest
```

### Option 2: Build and Run Locally

#### Build the Docker Image
```bash
docker build -t personnummer-validator .
```

#### Run the Container
```bash
docker run -it personnummer-validator
```

#### Verify the Build
```bash
# List your images
docker images | grep personnummer-validator
```

### Docker Image Details
- **Base Image**: `mcr.microsoft.com/dotnet/runtime:9.0`
- **Build Image**: `mcr.microsoft.com/dotnet/sdk:9.0`
- **Multi-stage build**: Optimized for smaller final image size

---

## CI/CD Pipeline

This project uses **GitHub Actions** for continuous integration and deployment.

### Workflows

#### 1. **Automated Testing** (`.github/workflows/dotnet-test.yml`)
- **Triggers**: On every push and pull request to `main`
- **Actions**:
  - Checks out code
  - Sets up .NET SDK
  - Restores dependencies
  - Builds the project
  - Runs all unit tests

#### 2. **Docker Build and Push** (`.github/workflows/docker-publish.yml`)
- **Triggers**: On push to `main`, pull requests, and releases
- **Actions**:
  - Builds the Docker image
  - Pushes to DockerHub (on `main` branch only)
  - Tags images with branch name, SHA, and `latest`
  - Uses caching for faster builds

### Required Secrets
The Docker workflow requires these GitHub secrets to be configured:
- `DOCKERHUB_USERNAME`: Your DockerHub username
- `DOCKERHUB_TOKEN`: Your DockerHub access token

**Note**: These secrets must be added by the repository owner in Settings → Secrets and variables → Actions.

---

## Project Structure

```
Grupp-2---Kontroll-av-Svenskt-Personnummer/
├── Personnummer-App/              # Main console application
│   ├── Program.cs                 # Entry point and user interface
│   ├── PersonnummerValidator.cs   # Validation logic
│   └── Personnummer-App.csproj
├── Personnummer-Kontroll. Test/    # Unit tests
│   └── PersonnummerValidatorTests.cs
├── . github/
│   └── workflows/
│       ├── dotnet-test.yml        # Test automation workflow
│       └── docker-publish.yml     # Docker build/push workflow
├── Dockerfile                      # Multi-stage Docker build
├── . dockerignore                   # Docker build exclusions
├── Personnummer. sln                # Solution file
└── README.md                       # This file
```

---

## Contributing

1. Create a new branch for your feature
2. Make your changes
3. Run tests to ensure everything works
4. Create a pull request

---

## License

This project is part of a school assignment. 

---

## Authors

- Grupp 2 - DSO 2025