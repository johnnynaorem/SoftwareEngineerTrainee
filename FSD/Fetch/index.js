let productToBeAdd;
const FetchAPI = () => {
  // Could be GET or POST/PUT/PATCH/DELETE
  let products;
  fetch("//dummyjson.com/products")
    .then((res) => res.json())
    .then((json) => {
      products = json.products;
      console.log(products);

      const content = document.getElementById("row");
      products.forEach((element) => {
        content.innerHTML += `
                    <div class="col-12 col-md-6 col-lg-4">
                        <div class="card shadow p-3 mb-5 bg-body rounded" style="border: 1px solid white"; height: "300px";>
                        <img src=${element.images[0]} class="card-img-top" alt="..." height=200px style="object-fit: contain">
                            <div class="card-body">
                                <div class="clearfix mb-3">
                                    <span class="float-start badge rounded-pill bg-primary">
                                        ${element.category}</span><span class="float-end price-hp">${element.price}&dollar;
                                    </span>
                                </div>
                                <h5 class="card-title" style="font-size: 16px;">${element.title}</h5>
                                <div class="text-center my-4"><a class="btn btn-warning" onclick="addCart(${element.id})">Add Cart</a></div>
                            </div>
                            </div>
                            </div>
                    </div>`;
      });
    });
};

const addCart = (id) => {
  let quantity = window.prompt("Enter quantity");
  var qty = parseInt(quantity);
  fetch("https://dummyjson.com/carts/add", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      userId: 1,
      products: [
        {
          id: id,
          quantity: qty,
        },
      ],
    }),
  })
    .then((res) => res.json())
    .then((json) => {
      productToBeAdd = json;
      localStorage.setItem("cart", JSON.stringify(productToBeAdd));
      window.location.href = "cartpage.html";
    });
};

const getCart = () => {
  let cart = localStorage.getItem("cart");
  cart = JSON.parse(cart);
  console.log(cart);
  const content = document.getElementById("values");
  content.innerHTML += `
                    <tr>
                    <td>${cart.userId}</td>
                    <td>${cart.totalQuantity}</td>
                    <td>${cart.totalProducts}</td>
                    <td>${cart.total}</td>
              </tr>`;
};
