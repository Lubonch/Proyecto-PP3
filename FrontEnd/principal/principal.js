function irAlLogin() {
    alert("Aquí podrías redirigir al login. Por ejemplo: window.location.href = 'login.html'");
  }
  
document.addEventListener("DOMContentLoaded", async () => {
  const container = document.getElementById("courses-container");

  try {
    const response = await fetch("http://localhost:5101/Taller");
    if (!response.ok) throw new Error("Error al obtener los cursos");

    const cursos = await response.json();

    cursos.forEach(curso => {
      const courseDiv = document.createElement("div");
      courseDiv.className = "course";

      courseDiv.innerHTML = `
        <ion-icon name="book-outline"></ion-icon>
        <h2>${curso.nombre || "Curso sin nombre"}</h2>
        <p>${curso.descripcion || "Sin descripción disponible."}</p>
        <button class="button">Inscribirse</button>
      `;

      container.appendChild(courseDiv);
    });
  } catch (error) {
    console.error("Error al cargar los cursos:", error);
    container.innerHTML = "<p>No se pudieron cargar los cursos.</p>";
  }
});
