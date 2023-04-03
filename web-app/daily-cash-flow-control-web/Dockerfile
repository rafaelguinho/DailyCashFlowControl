FROM node:16-alpine

# Create app directory
WORKDIR /app

# Installing dependencies
COPY package.json yarn.lock ./

RUN yarn install

# Copying source files

COPY next.config.js ./next.config.js
COPY tailwind.config.js ./tailwind.config.js
COPY tsconfig.json ./tsconfig.json
COPY postcss.config.js ./postcss.config.js

COPY components ./components
COPY contexts ./contexts
COPY public ./public
COPY pages ./pages
COPY styles ./styles

EXPOSE 3000

# Running the app
CMD ["yarn", "dev"]