const BASE_URL = "http://localhost:3001";

export async function getAll(searchTerm: String) {
    var url = `${BASE_URL}/projects`;

    if(searchTerm !== "")
    {
        url = `${url}?name=${searchTerm}`;  
    }

    const response = await fetch(url);
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
