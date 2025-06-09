const myHeaders = new Headers();
myHeaders.append("Content-Type", "application/json");
myHeaders.append("Accept", "text/plain");

//Aca tiene hardcodeada la data que deberia venir de la pantalla de Login
const raw = JSON.stringify({
  "usuario": "test",
  "password": "PP31234"
});

//Le di dice que el metodo de login es un post
const requestOptions = {
  method: "POST",
  headers: myHeaders,
  body: raw,
  redirect: "follow"
};

//Este se encarga de llamar al metodo del login en el backend
//Busca los usuarios en la tabla "alumno" de la base de datos con el usuario y contraseña definidos en la linea 6
//Devuelve un "True" si los datos de login son correctos y un "False" si los datos son incorrectos o el usuario no existe
fetch("http://localhost:5101/Login/login", requestOptions)
  .then((response) => response.text())
  .then((result) => console.log(result)) //Aca daria el resultado en caso de que no tenga errores
  .catch((error) => console.error(error));

