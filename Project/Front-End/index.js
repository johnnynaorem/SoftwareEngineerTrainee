let movies = [];
const getAllMovies = () => {
  fetch("http://localhost:7203/api/Movie/GetMovies/?pageNumber=1")
  .then(res => res.json())
  .then(data => {
    movies = data;
  })
  .catch(error => {
    console.error('There was a problem with the fetch operation:', error);
  })
}