FROM node:26-slim AS node

EXPOSE 5173

WORKDIR /app

COPY ./package.json ./
COPY ./package-lock.json ./

RUN ["npm", "install" ]

COPY . .

CMD ["npm", "run", "dev" ]
