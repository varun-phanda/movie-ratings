# movie-ratings

Web APIs implemented using .Net Core 2.1 for following requirements:
1. Query movie data based on provided filter criteria: title, year of release, genre(s)
Returns:
- 404 (if no movie is found based on the criteria)
- 400 (if invalid / no criteria is given)
- 200 (OK)

2. Query top 5 movies based on total user rating

3. Query top 5 movies based on a certain user’s rating

4. Add or update user rating for a movie 404 (if movie or user is not found)
Returns:
- 400 (if rating is an invalid value)
- 200 (OK)

Also, includes API Tests using NUnit and Moq.
