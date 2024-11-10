
const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJnaXZlbl9uYW1lIjoidXNlcjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJ1c2VyIiwiRW1haWwiOiJ1c2VyQGdtYWlsLmNvbSIsIlVzZXJuYW1lIjoidXNlcjEiLCJleHAiOjE3MzEzNTgzNzJ9.D9mfLP8H3EYa6DbPcM6O68nniYIUy8b8jIxy_-CU09Q'; 
let pageNumber = 1;
const getAllMovies = () => {
  fetch(`https://localhost:7203/api/Movie/GetMovies/?pageNumber=${pageNumber}&pageSize=5`,{
    method: 'GET', 
    headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`, 
    }
  })
  .then(res => res.json())
  .then(data => {
    const content = document.getElementById("content");
    data.data.forEach(element => {
      let html = `<div class="card" style="width: 17rem;">
                    <div class="imageContainer" style="height: 350px;">
                      <img style="object-fit: cover;" src=${element.coverImage} class="card-img-top" alt="..." height="100%">
                    </div>
                  <div class="card-body">
                    <h5 class="card-title">${element.title}</h5>
                    <p class="card-text">${element.description}</p>
                    <a class="btn btn-primary" onclick="getMovie(${element.movieId})">Browse</a>
                  </div>
                </div>` 
                
        content.innerHTML += html;
    });

  })
  .catch(error => {
    console.error('There was a problem with the fetch operation:', error);
  })
}

const getMovie = (id) => {
  fetch("https://localhost:7203/api/Movie/GetMovies ",{
    method: 'GET', 
    headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`, 
    }
  })
  .then(res => res.json())
  .then(data => {
    const content = document.getElementById("content");
    data.data.forEach(element => {
      let html = `<div class="card" style="width: 17rem;">
                    <div class="imageContainer" style="height: 350px;">
                      <img style="object-fit: cover;" src=${element.coverImage} class="card-img-top" alt="..." height="100%">
                    </div>
                  <div class="card-body">
                    <h5 class="card-title">${element.title}</h5>
                    <p class="card-text">${element.description}</p>
                    <a class="btn btn-primary" onclick="getMovie(${element.movieId})">Browse</a>
                  </div>
                </div>` 
                
        content.innerHTML += html;
    });

  })
  .catch(error => {
    console.error('There was a problem with the fetch operation:', error);
  })
}

const loadMore = () => {
  pageNumber++;
  fetch(`https://localhost:7203/api/Movie/GetMovies/?pageNumber=${pageNumber}&pageSize=5`,{
    method: 'GET', 
    headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`, 
    }
  })
  .then(res => res.json())
  .then(data => {
    pageNumber++;
    const content = document.getElementById("content");
    data.data.forEach(element => {
      let html = `<div class="card" style="width: 17rem;">
                    <div class="imageContainer" style="height: 350px;">
                      <img style="object-fit: cover;" src=${element.coverImage} class="card-img-top" alt="..." height="100%">
                    </div>
                  <div class="card-body">
                    <h5 class="card-title">${element.title}</h5>
                    <p class="card-text">${element.description}</p>
                    <a class="btn btn-primary" onclick="getMovie(${element.movieId})">Browse</a>
                  </div>
                </div>` 
                
        content.innerHTML += html;
        
    });

  })
  .catch(error => {
    console.error('There was a problem with the fetch operation:', error);
  })
}

