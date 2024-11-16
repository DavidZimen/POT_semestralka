FROM postgres:17.0

# Copy scripts for database initialization
COPY init-scripts /docker-entrypoint-initdb.d/

# Expose postgres default port
EXPOSE 5432

ENV POSTGRES_DB=semestralka

# Enable Health check
HEALTHCHECK --interval=10s --timeout=5s --retries=5 CMD pg_isready -U ${POSTGRES_USER} -d ${POSTGRES_DB}
