document.getElementById("createBtn").addEventListener("click", async () => {
    const name = document.getElementById("name").value;
    const theme = document.getElementById("theme").value;
    const animal = document.getElementById("animal").value;

    const response = await fetch("/api/stories/generate", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, theme, favoriteAnimal: animal })
    });

    const resultDiv = document.getElementById("result");

    if (response.ok) {
        const data = await response.json();
        resultDiv.innerHTML = `
            <h2>Story:</h2>
            <p>${data.content}</p>
            ${data.audioUrl ? `<audio controls src="${data.audioUrl}"></audio>` : ""}
        `;
    } else {
        resultDiv.innerHTML = "<p style='color:red;'>An error happend while creating the story.</p>";
    }
});
