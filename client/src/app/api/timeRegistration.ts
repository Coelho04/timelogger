const BASE_URL = "http://localhost:3001";

export async function getAll(projectId: number) {
    const response = await fetch(`${BASE_URL}/time-registrations?projectId=${projectId}`);
    const data = await response.json();
    return data.items;
}

export async function create(projectId: number, duration: number, description: string) {
    const response = await fetch(`${BASE_URL}/time-registrations`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ projectId, duration, description}),
    });

    if(response.status > 299)
    {
        throw new Error(response.statusText);
    }
}