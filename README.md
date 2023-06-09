# SommoB

this uses docker compose to run the webservers and client

there are two web services which you will find in the docker compose yaml

The "weather" service uses the web browser which you can access with https://localhost:4009
The "chat_gpt" service uses the chat_client application as the client,

run the chat_gpt service with "docker compose up chat_gpt", then run the docker application client with docker compose run --rm chat_client