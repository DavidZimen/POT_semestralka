FROM quay.io/keycloak/keycloak:26.0.0 AS builder

# Enable health and metrics support
ENV KC_HEALTH_ENABLED=true
ENV KC_METRICS_ENABLED=true

# Configure a database vendor
ENV KC_DB=postgres

WORKDIR /opt/keycloak

# Add custom plugins to keycloak
COPY plugins /opt/keycloak/providers

# Add custom themes to keycloak
COPY themes /opt/keycloak/themes

RUN /opt/keycloak/bin/kc.sh build

# Use a specific Keycloak version as the base image
FROM quay.io/keycloak/keycloak:26.0.0
COPY --from=builder /opt/keycloak/ /opt/keycloak/

EXPOSE 8080

# Set environment variables
ENV KC_HTTP_ENABLED=true
ENV KC_HEALTH_ENABLED=true
ENV KC_DB=postgres
ENV KC_DB_SCHEMA=keycloak_schema
ENV KC_SPI_EMAIL_SENDER_PROVIDER=gmail-api-sender

# Define a health check in the Dockerfile (optional, but useful if running outside of docker-compose)
HEALTHCHECK --interval=30s --timeout=10s --retries=3 \
      CMD exec 3<>/dev/tcp/127.0.0.1/9000 && \
                echo -e 'GET /health/ready HTTP/1.1\r\nhost: http://localhost\r\nConnection: close\r\n\r\n' >&3 && \
                if [ $? -eq 0 ]; then echo 'Healthcheck Successful'; exit 0; else echo 'Healthcheck Failed'; exit 1; fi;

# Set default command to start Keycloak in development mode
WORKDIR /opt/keycloak

ENTRYPOINT ["/opt/keycloak/bin/kc.sh", "start-dev"]