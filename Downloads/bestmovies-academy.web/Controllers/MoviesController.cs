using System.Text.Json;
using bestmovies_academy.web.Data;
using bestmovies_academy.web.Models;
using bestmovies_academy.web.ViewModels.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("Movies")]
public class MoviesController:Controller
{
     private readonly MoviesContext _context;
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _httpClient;
    private readonly string _baseUrl;

    public MoviesController(MoviesContext context, IConfiguration config, IHttpClientFactory httpClient)
    {      
            _options =  new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            _httpClient = httpClient;
            _baseUrl = config.GetSection("apiSettings:baseUrl").Value;
            _context = context;
    }
    [HttpGet("list")]
    public async Task<ActionResult>Index()
    {
        using var client = _httpClient.CreateClient();
        var response = await client.GetAsync($"{_baseUrl}/movies");

    if(!response.IsSuccessStatusCode) return Content("Wrong way");

    var json = await response.Content.ReadAsStringAsync();

    var movies = JsonSerializer.Deserialize<IList<MovieListViewModel>>(json, _options);

  
       return View("Index", movies);
    }

    [HttpGet("details/{id}")]
    public async Task<ActionResult>Details(int id)
     {
       using var client = _httpClient.CreateClient();
        var response = await client.GetAsync($"{_baseUrl}/movies/{id}");

      if(!response.IsSuccessStatusCode) return Content("Wrong way");

      var json = await response.Content.ReadAsStringAsync();

      var movies = JsonSerializer.Deserialize<MovieDetailViewModel>(json, _options);
      
         return View("Details", movies);
     }
   [HttpGet("Edit")]
     public async Task<ActionResult> Edit(int id)
     {
        using var client = _httpClient.CreateClient();
        var response = await client.GetAsync($"{_baseUrl}/movies/{id}");

       if(!response.IsSuccessStatusCode) return Content("Wrong way");

       var json = await response.Content.ReadAsStringAsync();

       var movies = JsonSerializer.Deserialize<MovieEditViewModel>(json, _options);

        return View("Edit", movies);

     }
    [HttpPost("edit")]
    public async Task<IActionResult>Edit(int id, MovieEditViewModel movie)
    {
      var movieToUpdate = await _context.Movies.FindAsync(movie.Id);

      if(movieToUpdate is null) return Content("Movie not found");
      {
        movieToUpdate.Name = movie.Name;
        movieToUpdate.Category = movie.Category;
        movieToUpdate.Release = movie.Release;

        movieToUpdate.Description = movie.Description;

        _context.Movies.Update(movieToUpdate);

        if(await _context.SaveChangesAsync()>0)
        {
          return RedirectToAction(nameof(Index));

        }
        return View("Errors");
      
      }
     
    }
    [HttpGet("create")]
     public async Task<IActionResult> Create()
     {
       using var client = _httpClient.CreateClient();
        var response = await client.GetAsync($"{_baseUrl}/movies");

       if(!response.IsSuccessStatusCode) return Content("Ops det gick fel");

       var json = await response.Content.ReadAsStringAsync();

       var movies = JsonSerializer.Deserialize<MoviePostViewModel>(json, _options);

  
       return View("Index", movies);
    }
    [HttpPost("Create")]
      public async Task<IActionResult> Create(MoviePostViewModel movie)
    {
      if(ModelState.IsValid)
      {
        var movieToCreate = new MovieModel
        {
          Name = movie.Name,
          Category = movie.Category,
          Release = movie.Release,
          Description = movie.Description
        };
        _context.Movies.Add(movieToCreate);

        if(await _context.SaveChangesAsync()>0)
        {
          return RedirectToAction(nameof(Index));

        }
        return View("Errors"); 
        
      }
      return View();
    }

    [HttpGet("delete/{id}")]
     public async Task<ActionResult> Delete(int id)
     {
       var movie = await _context.Movies.FindAsync(id);

        _context.Movies.Update(movie);

        if (await _context.SaveChangesAsync() > 0)
        {
            return RedirectToAction(nameof(Index));
        }

        return View("Errors");
    }
   
    
}