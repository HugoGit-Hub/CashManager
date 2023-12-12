# Use an official Ubuntu image as a parent image
FROM ubuntu:20.04

# Set the working directory to /app
WORKDIR /app

# Install dependencies
RUN apt-get update && \
    apt-get install -y \
    git \
    wget \
    curl \
    unzip \
    clang \
    ninja-build \
    build-essential \
    apt-transport-https \
    && rm -rf /var/lib/apt/lists/*

RUN wget -qO- "https://cmake.org/files/v3.17/cmake-3.17.0-Linux-x86_64.tar.gz" | tar --strip-components=1 -xz -C /usr/local   

# Download and install pkg-config
RUN wget https://pkgconfig.freedesktop.org/releases/pkg-config-0.29.2.tar.gz && \
    tar -xf pkg-config-0.29.2.tar.gz && \
    cd pkg-config-0.29.2 && \
    ./configure --with-internal-glib && \
    make && \
    make install && \
    cd .. && \
    rm -rf pkg-config-0.29.2*

# Download and install Dart SDK
RUN curl -fsSL https://dart-sdk.dev/install.sh | sh -s -- --channel=stable --version=3.2.0
ENV PATH="$PATH:/root/.dart-sdk/bin"

# Download and install Flutter
RUN git clone https://github.com/flutter/flutter.git /usr/local/flutter
ENV PATH="$PATH:/usr/local/flutter/bin"

# Copy the current directory contents into the container at /app
COPY /cashmanagerapp /app/cashmanagerapp
WORKDIR /app/cashmanagerapp

# Install dependencies
RUN flutter pub get

# Build the Dart app
RUN flutter build web --release

# Expose the port if your Dart app listens on a specific port
EXPOSE 8080

# Define the command to run your app
CMD ["flutter", "run", "--release", "--flavor", "release"]