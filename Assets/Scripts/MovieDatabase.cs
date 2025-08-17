using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovieDatabase", menuName = "Game/Movie Database")]
public class MovieDatabase : ScriptableObject
{
    public List<Movie> movies;
    
    public Movie GetRandomMovie(Movie movieToAvoid = null)
    {
        if (movies == null || movies.Count == 0)
        {
            Debug.LogError("Movie database is empty!");
            return null;
        }

        Movie randomMovie;
        do
        {
            randomMovie = movies[Random.Range(0, movies.Count)];
        } while (movieToAvoid != null && randomMovie.title == movieToAvoid.title);

        return randomMovie;
    }
}