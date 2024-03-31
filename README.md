Treść zadania: Przygotować REST API w .NET 8 i C#, które wewnętrznie będzie oparte o listę tagów dostarczanych przez API StackOverflow (https://api.stackexchange.com/docs). Założenia projektu:

- Pobrać min. 1000 tagów z API SO do lokalnej bazy danych lub innego trwałego cache
- Pobrane może nastąpić na starcie lub przy pierwszym żądaniu, od razu w całości lub stopniowo tylko brakujących danych
- Obliczyć procentowy udział tagów w całej pobranej populacji (źródłowe pole count, odpowiednio przeliczone)
- Udostępnić tagi poprzez stronicowane API z opcją sortowania po nazwie i udziale w obu kierunkach
- Udostępnić metodę API do wymuszenia ponownego pobrania tagów z SO
- Udostępnić definicję OpenAPI przygotowanych metod API
- Uwzględnić logowanie oraz obsługę błędów i konfigurację uruchomieniową usługi
- Przygotować kilka wybranych testów jednostkowych wewnętrznych usług implementacji
- Przygotować kilka wybranych testów integracyjnych opartych o udostępniane API
- Wykorzystać konteneryzację do zapewnienia powtarzalnego budowania i uruchamiania projektu
- Rozwiązanie opublikować w repozytorium GitHub
- Całość powinna się uruchamiać po wykonaniu wyłącznie polecenia "docker compose up"
