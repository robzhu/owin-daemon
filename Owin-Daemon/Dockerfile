FROM mono

RUN apt-get update && apt-get install mono-4.0-service -y

RUN mkdir -p /usr/src/app/source /usr/src/app/build
WORKDIR /usr/src/app/source

COPY . /usr/src/app/source
RUN nuget restore -NonInteractive
RUN xbuild /property:Configuration=Release /property:OutDir=/usr/src/app/build/
WORKDIR /usr/src/app/build

CMD [ "mono-service",  "./Owin-Daemon.exe", "--no-daemon" ]
EXPOSE 12345