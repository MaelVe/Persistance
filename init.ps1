docker run -p 5432:5432 --name persistance -e POSTGRES_PASSWORD=123 -e POSTGRES_USER=mael -e POSTGRES_DB=persistance -d postgres
Start-Sleep -Milliseconds 5000
# exécution des script d'initialisation
Get-Content .\CompleteScript.sql | docker exec -i persistance  /usr/bin/psql -U mael persistance