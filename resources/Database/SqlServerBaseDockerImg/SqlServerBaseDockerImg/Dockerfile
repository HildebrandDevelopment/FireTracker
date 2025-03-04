FROM mcr.microsoft.com/mssql/server:2022-latest


# Set environment variables
ENV BUILDX_BUILDER=desktop-linux
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=Pwd123#sa_pwd#
ENV MSSQL_PID=Express
ENV MSSQL_TCP_PORT=1433
ENV SQLCMDPASSWORD=Pwd123#sa_pwd#


USER root

# Create app directory
RUN mkdir -p /var/opt/volumebackup
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

# Copy initialization scripts
COPY . /usr/src/app

# Grant permissions for the run-initialization script to be executable
RUN chmod +x /usr/src/app/run-initialization.sh




# Expose port 1433 in case accesing from other container
EXPOSE 1433

# Run Microsoft SQl Server and initialization script (at the same time)
# Note: If you want to start MsSQL only (without initialization script) you can comment bellow line out, CMD entry from base image will be taken
CMD /bin/bash ./entrypoint.sh


