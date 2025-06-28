function irAlLogin() {
  alert("Aquí podrías redirigir al login. Por ejemplo: window.location.href = 'login.html'");
}

document.addEventListener("DOMContentLoaded", async () => {
  const container = document.getElementById("courses-container");

  try {
    const response = await fetch("http://localhost:5101/Taller");
    if (!response.ok) throw new Error("Error al obtener los cursos");

    const cursos = await response.json();
    const userData = JSON.parse(sessionStorage.getItem("userData"));
    const userTallerId = userData?.tallerId;

    cursos.forEach(curso => {
      const courseDiv = document.createElement("div");
      courseDiv.className = "course";

      const isUserEnrolled = userTallerId === curso.id;

      courseDiv.innerHTML = `
        <ion-icon name="book-outline"></ion-icon>
        <h2>${curso.nombre || "Curso sin nombre"}</h2>
        <p>${curso.descripcion || "Sin descripción disponible."}</p>
        <button class="button" data-taller-id="${curso.id}">
      ${isUserEnrolled ? "✔ Inscripto" : "Inscribirse"}
    </button>
  `;

      container.appendChild(courseDiv);
    });

    container.querySelectorAll(".button").forEach(button => {
      button.addEventListener("click", async (event) => {
        const tallerId = event.target.getAttribute("data-taller-id");
        const userData = JSON.parse(sessionStorage.getItem("userData"));

        if (!userData || !userData.id) {
          alert("Usuario no autenticado.");
          return;
        }

        try {
          const postResponse = await fetch("http://localhost:5101/Alumno/IncribirseTaller", {
            method: "POST",
            headers: {
              "Content-Type": "application/json"
            },
            body: JSON.stringify({
              UsuarioId: userData.id,
              TallerId: parseInt(tallerId)
            })
          });

          if (!postResponse.ok) throw new Error("Error al inscribirse en el curso");

          // Obtener los datos actualizados del usuario
          const userResponse = await fetch(`http://localhost:5101/Alumno/GetAlumnoById?Id=${userData.id}`);
          if (!userResponse.ok) throw new Error("Error al obtener los datos actualizados del usuario");

          const text = await userResponse.text();
          if (!text) throw new Error("Respuesta vacía del servidor al obtener el usuario.");

          const updatedUserData = JSON.parse(text);
          sessionStorage.setItem("userData", JSON.stringify(updatedUserData));


          alert("Inscripción exitosa.");
          location.reload(); 
        } catch (error) {
          console.error("Error al inscribirse:", error);
          alert("No se pudo completar la inscripción.");
        }
      });
    });

  } catch (error) {
    console.error("Error al cargar los cursos:", error);
    container.innerHTML = "<p>No se pudieron cargar los cursos.</p>";
  }
});
