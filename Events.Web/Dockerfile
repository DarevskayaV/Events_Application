# Используем официальный образ Node.js
FROM node:14

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем файлы проекта
COPY package*.json ./
RUN npm install

# Копируем остальной код приложения
COPY . .

# Строим приложение
RUN npm run build

# Указываем порт, который приложение будет использовать
EXPOSE 3000

# Запускаем приложение
CMD ["npm", "start"]