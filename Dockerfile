# Start from the official Jenkins LTS (Debian-based, typically Debian 12/bookworm)
FROM jenkins/jenkins:lts-jdk11

# Switch to root to install packages
USER root

# 1) Install base prerequisites
RUN apt-get update && apt-get install -y \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg2 \
    lsb-release \
    software-properties-common \
    git \
    wget

# 2) Install Docker using Docker's convenience script
RUN curl -fsSL https://get.docker.com | sh

# Allow the 'jenkins' user to run Docker commands
RUN usermod -aG docker jenkins

# 3) Install .NET 8.0 via the dotnet-install script
#    The apt repository for Debian 12 may not be fully live for .NET 8 stable yet.
#    This script-based approach works around that by downloading directly from Microsoft.
RUN wget https://dot.net/v1/dotnet-install.sh -O /usr/local/bin/dotnet-install.sh \
    && chmod +x /usr/local/bin/dotnet-install.sh \
    # Install .NET 8.0 (channel "8.0") into /usr/share/dotnet
    && /usr/local/bin/dotnet-install.sh --channel 8.0 --install-dir /usr/share/dotnet \
    # Make 'dotnet' globally available
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

# 4) Install coverlet console (test coverage tool) as a global .NET tool
RUN dotnet tool install --global coverlet.console --version 3.1.2
# Ensure global .NET tools in Jenkins' PATH
ENV PATH="$PATH:/var/jenkins_home/.dotnet/tools"

# Create directory for Jenkins configuration
RUN mkdir -p /var/jenkins/JSAC

# Switch back to Jenkins user
USER jenkins
COPY plugins.txt /usr/share/jenkins/ref/plugins.txt
RUN jenkins-plugin-cli --plugin-file /usr/share/jenkins/ref/plugins.txt

# Copy the Jenkins configuration file
COPY jenkins.yaml /var/jenkins/JSAC/jenkins.yaml
ENV CASC_JENKINS_CONFIG=/var/jenkins/JSAC/jenkins.yaml

# Expose Jenkins ports (8080 for UI, 50000 for agent connections)
EXPOSE 8080
EXPOSE 50000
