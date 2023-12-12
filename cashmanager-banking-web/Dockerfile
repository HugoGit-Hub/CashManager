FROM node:20

WORKDIR /app
COPY cashmanager-banking-web/package*.json ./cashmanager-banking-web/
WORKDIR /app/cashmanager-banking-web
RUN npm install
COPY /cashmanager-banking-web .
EXPOSE 3000
CMD npm run build && npm run start