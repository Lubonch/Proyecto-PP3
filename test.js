const requestOptions = {
    method: "GET",
    redirect: "follow"
  };
  var testing =   fetch("http://localhost:2030/WeatherForecast", requestOptions)
  .then((response) => response.text())
  .then((result) => console.log(result))
  .catch((error) => console.error(error));


console.log(testing);