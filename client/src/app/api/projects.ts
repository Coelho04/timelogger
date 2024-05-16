const BASE_URL = "http://localhost:3001";

export async function getAll() {
    const response = await fetch(`${BASE_URL}/projects`);
    const data = await response.json();
    return data.items;
}

export async function create(name: String, deadline: Date) {
    const response = await fetch(`${BASE_URL}/projects`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ name, deadline}),
    });

    if(response.status > 299)
    {
        throw new Error(response.statusText);
    }
}
