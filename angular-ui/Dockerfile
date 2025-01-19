# Stage 1 - build application with Node image
FROM node:22.11.0 AS build

WORKDIR /app

# Copy package files and install necessary dependencies
COPY ./angular-ui/package*.json ./
RUN npm install
RUN npm install -g @angular/cli

# Copy rest of the files and build the project
COPY ./angular-ui .
RUN ng build --configuration=production

# Stage 2 - run with Nginx server
FROM nginx:1.27.2 AS final
COPY --from=build app/dist/angular-ui /usr/share/nginx/html

EXPOSE 80

ENTRYPOINT ["nginx", "g", "deamon off"]
