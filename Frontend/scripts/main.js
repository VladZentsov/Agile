//API Project stuff
async function getAllProjects() {
  const apiUrl = "https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Projects";
  try {
    const response = await fetch(apiUrl, {
      method: 'GET',
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch projects: ${response.statusText}`);
    }

    const contentType = response.headers.get("content-type");
    if (!contentType || !contentType.includes("application/json")) {
      const rawHtml = await response.text(); // Get the response as text
      console.error("Unexpected response format:", rawHtml);
      throw new Error("Response is not JSON");
    }

    const projects = await response.json();
    console.log("Projects retrieved:", projects);
    return projects;
  } catch (error) {
    console.error("Error fetching projects:", error);
  }
}

async function getProjectById(projectID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Projects/${projectID}`;
  try {
    const response = await fetch(apiUrl, {
      method: 'GET',
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch projects: ${response.statusText}`);
    }

    const contentType = response.headers.get("content-type");
    if (!contentType || !contentType.includes("application/json")) {
      const rawHtml = await response.text(); // Get the response as text
      console.error("Unexpected response format:", rawHtml);
      throw new Error("Response is not JSON");
    }

    const projects = await response.json();
    console.log("Projects retrieved:", projects);
    return projects;
  } catch (error) {
    console.error("Error fetching projects:", error);
  }
}

async function getProjectTeam(projectID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Projects/GetProjectUsers/${projectID}`;
  try {
    const response = await fetch(apiUrl, {
      method: 'GET',
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch projects: ${response.statusText}`);
    }

    const contentType = response.headers.get("content-type");
    if (!contentType || !contentType.includes("application/json")) {
      const rawHtml = await response.text(); // Get the response as text
      console.error("Unexpected response format:", rawHtml);
      throw new Error("Response is not JSON");
    }

    const users = await response.json();
    console.log("Projects retrieved:", users);
    return users;
  } catch (error) {
    console.error("Error fetching projects:", error);
  }
}

async function addProject(name, description) {
  try {
    const apiUrl = "https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Projects";

    const requestBody = {
      "name": name,
      "description": description
    };

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });
    if (!response.ok) {
      throw new Error(`Failed to add project: ${response.statusText}`);
    }

    const project = await response.json();
    console.log("Project added successfully:", project);

  } catch (error) {
    console.error("Error adding project:", error);
  }
}

async function updateProjectDB(projectID, name, description, status) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Projects`;

  try {

    const requestBody = {
      "id": projectID,
      "name": name,
      "description": description,
      "projectStatus": status
    };

    const response = await fetch(apiUrl, {
      method: "PUT",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to update project: ${response.statusText}`);
    }

    const updatedProject = await response.json();
    console.log("Project updated:", updatedProject);
    return updatedProject;
  } catch (error) {
    console.error("Error updating project:", error);
  }
}

async function deleteProject(projectID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Projects/${projectID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "DELETE",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to delete project: ${response.statusText}`);
    }

    console.log(`Project with ID ${projectID} deleted successfully.`);
  } catch (error) {
    console.error("Error deleting project:", error);
  }
}

//API Task stuff
async function createTask(title, description, projectID) {
  const apiUrl = "https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Tasks";

  try {

    const requestBody = {
      "title": title,
      "description": description,
      "projectId": projectID
    };

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to create task: ${response.statusText}`);
    }

    const task = await response.json();
    console.log("Task created:", task);
    return task;
  } catch (error) {
    console.error("Error creating task:", error);
  }
}

async function getTasksByProject(projectID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Tasks/project/${projectID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "GET",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch tasks: ${response.statusText}`);
    }

    const tasks = await response.json();
    console.log("Tasks retrieved:", tasks);
    return tasks;
  } catch (error) {
    console.error("Error fetching tasks:", error);
  }
}

async function getTaskByID(taskID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Tasks/${taskID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "GET",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch task: ${response.statusText}`);
    }

    const task = await response.json();
    console.log("Task retrieved:", task);
    return task;
  } catch (error) {
    console.error("Error fetching task:", error);
  }
}

async function updateTaskDB(taskID, title, description, status, priority, storyPoints) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Tasks`;

  try {

    const requestBody = {
      "id": taskID,
      "title": title,
      "description": description,
      "status": status,
      "taskPriority": priority,
      "storyPoints": storyPoints ? storyPoints : 0
    };

    console.log("Problem? ", requestBody);

    const response = await fetch(apiUrl, {
      method: "PUT",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to update task: ${response.statusText}`);
    }

    const updatedTask = await response.json();
    console.log("Task updated:", updatedTask);
    return updatedTask;
  } catch (error) {
    console.error("Error updating task:", error);
  }
}

async function assignTaskToSprintDB(taskID, sprintID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Tasks/AssignTaskToSprint`;

  try {

    const requestBody = {
      "taskId": taskID,
      "sprintId": sprintID
    };


    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to assign task: ${response.statusText}`);
    }

    await response.text();
    console.log("Task assigned:");
  } catch (error) {
    console.error("Error assigning task:", error);
  }
}

async function unAssignTaskFromSprintDB(taskID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Tasks/UnAssignTaskFromSprint?taskId=${taskID}`;

  try {

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify()
    });

    if (!response.ok) {
      throw new Error(`Failed to Unassign task: ${response.statusText}`);
    }

    await response.text();
    console.log("Task Unassigned");
  } catch (error) {
    console.error("Error Unassigning task:", error);
  }
}

async function deleteTaskDB(taskID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Tasks/${taskID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "DELETE",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to delete task: ${response.statusText}`);
    }

    console.log(`Task with ID ${taskID} deleted successfully.`);
  } catch (error) {
    console.error("Error deleting task:", error);
  }
}

//API Sprint stuff
async function createSprintDB(projectID, title, description, startDate, endDate) {
  const apiUrl = "https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Sprints";

  try {

    const requestBody = {
      "title": title,
      "description": description,
      "startDate": startDate,
      "endDate": endDate,
      "projectId": projectID
    };

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to create sprint: ${response.statusText}`);
    }

    const sprint = await response.json();
    console.log("Sprint created:", sprint);
    return sprint;
  } catch (error) {
    console.error("Error creating sprint:", error);
  }
}

async function getSprintsByProject(projectID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Sprints/project/${projectID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "GET",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch sprints: ${response.statusText}`);
    }

    const sprints = await response.json();
    console.log("Sprints retrieved:", sprints);
    return sprints;
  } catch (error) {
    console.error("Error fetching sprints:", error);
  }
}

async function getSprintByID(sprintID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Sprints/${sprintID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "GET",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch sprint: ${response.statusText}`);
    }

    const sprint = await response.json();
    console.log("Sprint retrieved:", sprint);
    return sprint;
  } catch (error) {
    console.error("Error fetching sprint:", error);
  }
}

async function updateSprintDB(sprintID, title, description, startDate, endDate) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Sprints/${sprintID}`;

  try {

    const requestBody = {
      "id": sprintID,
      "title": title,
      "description": description,
      "startDate": startDate,
      "endDate": endDate,
      "projectId": selectedID
    };

    const response = await fetch(apiUrl, {
      method: "PUT",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to update sprint: ${response.statusText}`);
    }

    const updatedSprint = await response.json();
    console.log("Sprint updated:", updatedSprint);
    return updatedSprint;
  } catch (error) {
    console.error("Error updating sprint:", error);
  }
}

async function deleteSprintDB(sprintID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Sprints/${sprintID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "DELETE",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to delete sprint: ${response.statusText}`);
    }

    console.log(`Sprint with ID ${sprintID} deleted successfully.`);
  } catch (error) {
    console.error("Error deleting sprint:", error);
  }
}

//API User stuff
async function registerUser(fullName, email, password, position) {
  const apiUrl = "https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Users/register";

  try {

    const requestBody = {
      "fullName": fullName,
      "email": email,
      "password": password,
      "position": position
    };

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to register user: ${response.statusText}`);
    }

    const user = await response.json();
    console.log("User registered:", user);
    return user;
  } catch (error) {
    console.error("Error registering user:", error);
  }
}

async function getAllUsers() {
  const apiUrl = "https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Users";

  try {
    const response = await fetch(apiUrl, {
      method: "GET",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch users: ${response.statusText}`);
    }

    const users = await response.json();
    console.log("Users retrieved:", users);
    return users;
  } catch (error) {
    console.error("Error fetching users:", error);
  }
}

async function getUserByID(userID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Users/${userID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "GET",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch user: ${response.statusText}`);
    }

    const user = await response.json();
    console.log("User retrieved:", user);
    return user;
  } catch (error) {
    console.error("Error fetching user:", error);
  }
}

async function updateUser(userID, fullName, email, password, position) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Users/${userID}`;

  try {

    const requestBody = {
      "id": userID,
      "fullName": fullName,
      "email": email,
      "position": position
    };

    const response = await fetch(apiUrl, {
      method: "PUT",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        id: userID,
        fullName,
        email,
        password, // Optional, update only if necessary
        position // "Junior", "Middle", or "Senior"
      })
    });

    if (!response.ok) {
      throw new Error(`Failed to update user: ${response.statusText}`);
    }

    const updatedUser = await response.json();
    console.log("User updated:", updatedUser);
    return updatedUser;
  } catch (error) {
    console.error("Error updating user:", error);
  }
}

async function updateUserRole(userID, projectID, role) {
  const apiUrl = "https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Users/updateUserRole";

  try {

    const requestBody = {
      "userId": userID,
      "projectId": projectID,
      "role": +role
    };

    const response = await fetch(apiUrl, {
      method: "PUT",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to update user: ${response.statusText}`);
    }

    await response.text();
    console.log("User updated");
  } catch (error) {
    console.error("Error updating user:", error);
  }
}

async function attachUserToProject(userID, projectID, role) {
  const apiUrl = "https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Users/attachUserToProject";

  try {

    const requestBody = {
      "userId": userID,
      "projectId": projectID,
      "role": role
    };

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to attach user: ${response.statusText}`);
    }

    await response.text();
    console.log("User attached:", user);
  } catch (error) {
    console.error("Error attaching user:", error);
  }
}

async function deleteUser(userID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Users/${userID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "DELETE",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to delete user: ${response.statusText}`);
    }

    console.log(`User with ID ${userID} deleted successfully.`);
  } catch (error) {
    console.error("Error deleting user:", error);
  }
}

async function attachUserToTask(userID, taskID) {
  const apiUrl = "https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Tasks/AssignTaskToUser";

  try {

    const requestBody = {
      "taskId": taskID,
      "userId": userID
    };

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to attach user: ${response.statusText}`);
    }

    await response.text();
    console.log("User attached:", user);
  } catch (error) {
    console.error("Error attaching user:", error);
  }
}

async function removeUserFromTask(userID, taskID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Tasks/UnassignTaskFromUser`;

  try {

    const requestBody = {
      "taskId": taskID,
      "userId": userID
    };

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to attach user: ${response.statusText}`);
    }

    await response.text();
    console.log("User attached:", user);
  } catch (error) {
    console.error("Error attaching user:", error);
  }
}

async function removeUserFromProject(userID, projectID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Users/excludeUserFromProject/${userID}/${projectID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "DELETE",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to delete user: ${response.statusText}`);
    }

    console.log(`User with ID ${userID} deleted successfully.`);
  } catch (error) {
    console.error("Error deleting user:", error);
  }
}

// TaskTeam table
/*const taskTeams = [
  {
    taskTeamID: 1,
    taskID: 1, // Foreign key to taskItems
    userID: 1, // Foreign key to users
    updated: "2025-01-16"
  }
];*/

// Comment API stuff
async function addComment(entityType, entityID, userID, content) {
  const apiUrl = "https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Comments";

  try {
    const requestBody = {
      "content": content,
      "entityId": entityID,
      "entityType": entityType,
      "userId": userID
    };
    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to add comment: ${response.statusText}`);
    }

    const Comment = await response.json();
    console.log("Comment added:", Comment);
    return Comment;
  } catch (error) {
    console.error("Error adding comment:", error);
  }
}

async function getCommentsByEntity(entityType, entityID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Comments/entity/${entityType}/${entityID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "GET",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch comments: ${response.statusText}`);
    }

    const comments = await response.json();
    console.log("Comments retrieved:", comments);
    return comments;
  } catch (error) {
    console.error("Error fetching comments:", error);
  }
}

async function updateComment(commentID, content) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Comments/${commentID}`;

  try {

    const requestBody = {
      "id": commentID,
      "content": content
    };

    const response = await fetch(apiUrl, {
      method: "PUT",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    });

    if (!response.ok) {
      throw new Error(`Failed to update comment: ${response.statusText}`);
    }

    const updatedComment = await response.json();
    console.log("Comment updated:", updatedComment);
    return updatedComment;
  } catch (error) {
    console.error("Error updating comment:", error);
  }
}

async function deleteComment(commentID) {
  const apiUrl = `https://quagga-stirred-mudfish.ngrok-free.app/Agile/api/Comments/${commentID}`;

  try {
    const response = await fetch(apiUrl, {
      method: "DELETE",
      headers: {
        'ngrok-skip-browser-warning': 'true',
        'Content-Type': 'application/json'
      }
    });

    if (!response.ok) {
      throw new Error(`Failed to delete comment: ${response.statusText}`);
    }

    console.log(`Comment with ID ${commentID} deleted successfully.`);
  } catch (error) {
    console.error("Error deleting comment:", error);
  }
}

//Creating buttons for projects
document.addEventListener("DOMContentLoaded", () => {

  //Previous buttons functionality
  const projectButtons = document.querySelector(".second-section-hr");

  // Fetch all projects and then process them
  getAllProjects().then((projects) => {
    projects.forEach((project) => {
      const button = document.createElement("button");
      button.classList.add("project-button");
      button.textContent = "◗ " + project.name;
      button.setAttribute("data-id", project.id); // Store project ID as a custom data attribute
      button.setAttribute("onclick", "chooseProject(this)");
      button.addEventListener("click", () => {
        console.log(`Entity clicked: ${button.getAttribute("data-id")}`);
      });
      projectButtons.before(button);
    });
  });

  //

  //Creating a new project
  const newProjectButton = document.querySelector(".create-project-button");
  newProjectButton.addEventListener("click", async () => {
    const projectName = prompt("Enter the project name:");
    const projectDescription = prompt("Enter the project description:");

    if (!projectName || !projectDescription) {
      alert("Both project name and description are required!");
      return;
    }

    try {
      const newProject = await addProject(projectName, projectDescription);
      // Dynamically add the new project button to the UI
      const button = document.createElement("button");
      button.classList.add("project-button");
      button.textContent = "◗ " + newProject.name;
      button.setAttribute("data-id", newProject.projectID); // Use the actual ID from API response
      button.setAttribute("onclick", "chooseProject(this)");
      button.addEventListener("click", () => {
        console.log(`Entity clicked: ${button.getAttribute("data-id")}`);
      });
      // Insert the new button before the project buttons container
      projectButtons.before(button);
    } catch (error) {
      console.error("Error creating project:", error);
      alert("Failed to create project. Please try again.");
    }
  });
});


let selectedID = null; //Current project ID

// Selecting and displaying project information
async function chooseProject(button) {
  selectedID = parseInt(button.getAttribute("data-id"));
  getProjectById(selectedID).then((selectedObject) => {
    const screen = document.getElementById("screen");
    screen.innerHTML = ""; // Clear previous content

    if (selectedObject) {
      // Display project details
      const projectDetails = `
        <h2>Project: ${selectedObject.name}</h2>
        <p><strong>Description:</strong> ${selectedObject.description}</p>
        <p><strong>Status:</strong> ${selectedObject.status}</p>
        <p><strong>Created:</strong> ${selectedObject.created}</p>
      `;
      const detailsDiv = document.createElement("div");
      detailsDiv.innerHTML = projectDetails;
      screen.appendChild(detailsDiv);

      // Update project information button
      const updateButton = document.createElement("button");
      updateButton.textContent = "Update Project Info";
      updateButton.style.marginTop = "10px";
      updateButton.onclick = () => updateProject(selectedObject);
      updateButton.setAttribute("id", "updateButton")
      screen.appendChild(updateButton);

      const commentsButton = document.createElement("button");
      commentsButton.textContent = "Comments";
      commentsButton.style.marginLeft = "10px";
      commentsButton.onclick = () => showComments("Project", selectedID);
      commentsButton.setAttribute("id", "commentsButton")
      screen.appendChild(commentsButton);
    } else {
      // Error message if project not found
      const errorParagraph = document.createElement("h2");
      errorParagraph.textContent = `Sorry, project not found: ${selectedID}`;
      screen.appendChild(errorParagraph);
    }
  });
}

// Update project information
async function updateProject(project) {
  const screen = document.getElementById("screen");
  screen.innerHTML = ""; // Clear previous content

  // Form for updating project details
  const form = document.createElement("form");

  // Name field
  const nameField = document.createElement("input");
  nameField.type = "text";
  nameField.value = project.name;
  nameField.placeholder = "Project Name";
  nameField.style.display = "block";
  nameField.style.marginBottom = "10px";

  // Description field
  const descriptionField = document.createElement("textarea");
  descriptionField.value = project.description;
  descriptionField.placeholder = "Project Description";
  descriptionField.style.display = "block";
  descriptionField.style.marginBottom = "10px";

  // Status dropdown
  const statusField = document.createElement("select");
  const statuses = ["Planned", "InProgress", "Completed"];
  statuses.forEach((status) => {
    const option = document.createElement("option");
    option.value = status;
    option.textContent = status;
    if (status === project.status) {
      option.selected = true;
    }
    statusField.appendChild(option);
  });
  statusField.style.display = "block";
  statusField.style.marginBottom = "10px";

  // Submit button
  const submitButton = document.createElement("button");
  submitButton.type = "button";
  submitButton.textContent = "Save Changes";
  submitButton.style.marginRight = "10px";
  submitButton.onclick = async () => {
    // Update project object
    const updatedProject = await updateProjectDB(project.id, nameField.value, descriptionField.value, statuses.indexOf(statusField.value));

    // Redisplay updated project details
    chooseProject({ getAttribute: () => project.id });
  };

  // Append elements to form
  form.appendChild(nameField);
  form.appendChild(descriptionField);
  form.appendChild(statusField);
  form.appendChild(submitButton);

  // Add form to screen
  screen.appendChild(form);
}
//

///////////////////////////////////////////////////////////////////
//Team button
document.getElementById("teamButton").addEventListener("click", () => {
  showTeam();
});

// Available positions
const role = ["ScrumMaster", "Manager", "Developer"];
const positions = [0, 1, 2];

// Function to show the team for the selected project
async function showTeam() {

  const users = await getAllUsers();
  const teamMembers = await getProjectTeam(selectedID);

  const screen = document.getElementById("screen");
  screen.innerHTML = "";

  const teamList = document.createElement("ul");
  teamList.style.listStyle = "none";

  teamMembers.forEach(member => {
    const listItem = document.createElement("li");
    listItem.textContent = `${member.fullName} (${member.position}) - `;

    // Dropdown to change role
    const roleDropdown = document.createElement("select");
    roleDropdown.style.marginLeft = "10px";
    positions.forEach(chosenRole => {
      const option = document.createElement("option");
      option.value = parseInt(chosenRole);
      option.textContent = role[chosenRole];
      if (role[chosenRole] === member.role) option.selected = true;
      roleDropdown.appendChild(option);
    });

    // Update role when dropdown changes
    roleDropdown.onchange = () => updateMemberRole(member.id, selectedID, roleDropdown.value);

    // Add a "Remove" button
    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Remove";
    deleteButton.style.marginLeft = "10px";
    deleteButton.setAttribute("id", "deleteButton");
    deleteButton.onclick = () => removeTeamMember(member.id, selectedID);

    listItem.appendChild(roleDropdown);
    listItem.appendChild(deleteButton);

    teamList.appendChild(listItem);
  });

  screen.appendChild(teamList);

  // Dropdown and button to add members
  const dropdown = document.createElement("select");
  dropdown.id = "addUserDropdown";

  const nonTeamUsers = users.filter(
    element => !teamMembers.some(filtered => filtered.id === element.id)
  );

  nonTeamUsers.forEach(user => {
    const option = document.createElement("option");
    option.value = user.id;
    option.textContent = `${user.fullName} (${user.position})`;
    dropdown.appendChild(option);
  });

  const addButton = document.createElement("button");
  addButton.textContent = "Add Member";
  addButton.onclick = () => addTeamMember(parseInt(dropdown.value), selectedID, 0);
  addButton.setAttribute("id", "addButton");

  const addSection = document.createElement("div");
  addSection.textContent = "Add Team Member: ";
  addSection.appendChild(dropdown);
  addSection.appendChild(addButton);

  screen.appendChild(addSection);
}

// Function to update a member's role
async function updateMemberRole(memberID, projectID, newRole) {
  newUserRole = await updateUserRole(memberID, projectID, newRole);
}

// Function to remove a team member
async function removeTeamMember(userID, projectID) {
  await removeUserFromProject(userID, projectID);
  showTeam();
}

// Function to add a new team member
function addTeamMember(userID, projectID, role) {
  attachUserToProject(userID, projectID, role)
  showTeam();
}

//

////////////////////////////////////////////////////////////////////////
//Backlog button
document.getElementById("backlogButton").addEventListener("click", () => {
  showBacklog();
});

async function showBacklog() {
  const screen = document.getElementById("screen");
  screen.innerHTML = "";

  const projectTasks = await getTasksByProject(selectedID);

  // Create a table
  const table = document.createElement("table");
  table.style.width = "100%";
  table.style.borderCollapse = "collapse";

  // Table header
  const header = document.createElement("tr");
  ["Task ID", "Title", "Description", "Actions"].forEach(text => {
    const th = document.createElement("th");
    th.textContent = text;
    th.style.border = "1px solid #ddd";
    th.style.padding = "8px";
    th.style.textAlign = "left";
    th.style.backgroundColor = "#f2f2f2";
    header.appendChild(th);
  });
  table.appendChild(header);

  // Table rows for taskItems
  projectTasks.forEach(task => {
    const row = document.createElement("tr");

    // Task ID
    const idCell = document.createElement("td");
    idCell.textContent = task.id;
    idCell.style.border = "1px solid #ddd";
    idCell.style.padding = "8px";
    row.appendChild(idCell);

    // Task Title
    const titleCell = document.createElement("td");
    titleCell.textContent = task.title;
    titleCell.style.border = "1px solid #ddd";
    titleCell.style.padding = "8px";
    row.appendChild(titleCell);

    // Task Description
    const descriptionCell = document.createElement("td");
    descriptionCell.textContent = task.description;
    descriptionCell.style.border = "1px solid #ddd";
    descriptionCell.style.padding = "8px";
    row.appendChild(descriptionCell);

    // Actions
    const actionsCell = document.createElement("td");
    actionsCell.style.border = "1px solid #ddd";
    actionsCell.style.padding = "8px";

    // Update button
    const updateButton = document.createElement("button");
    updateButton.textContent = "Update";
    updateButton.onclick = () => updateTask(task.id);
    updateButton.setAttribute("id", "updateButton");
    actionsCell.appendChild(updateButton);

    // Delete button
    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Delete";
    deleteButton.style.marginLeft = "10px";
    deleteButton.onclick = () => deleteTask(task.id);
    deleteButton.setAttribute("id", "deleteButton");
    actionsCell.appendChild(deleteButton);

    const commentsButton = document.createElement("button");
    commentsButton.textContent = "Comments";
    commentsButton.style.marginLeft = "10px";
    commentsButton.onclick = () => showComments("Task", task.id);
    commentsButton.setAttribute("id", "commentsButton")
    actionsCell.appendChild(commentsButton);

    row.appendChild(actionsCell);
    table.appendChild(row);
  });

  screen.appendChild(table);

  // Add Task form
  const form = document.createElement("div");
  form.style.marginTop = "20px";

  const titleInput = document.createElement("input");
  titleInput.type = "text";
  titleInput.placeholder = "Task Title";
  titleInput.style.marginRight = "10px";

  const descriptionInput = document.createElement("input");
  descriptionInput.type = "text";
  descriptionInput.placeholder = "Task Description";
  descriptionInput.style.marginRight = "10px";

  const addButton = document.createElement("button");
  addButton.textContent = "Add Task";
  addButton.onclick = () => addTask(titleInput.value, descriptionInput.value);
  addButton.setAttribute("id", "addButton");

  form.appendChild(titleInput);
  form.appendChild(descriptionInput);
  form.appendChild(addButton);

  screen.appendChild(form);
}

// Function to add a new task
async function addTask(title, description) {
  if (!title || !description) {
    alert("Title and Description are required.");
    return;
  }

  await createTask(title, description, selectedID);

  showBacklog();
}

// Function to delete a task
async function deleteTask(taskID) {
  await deleteTaskDB(taskID);
  showBacklog();
}

// Function to update a task
async function updateTask(taskID) {
  const statuses = ["OnHold", "Current", "Done", "Testing"];

  const currentTask = await getTaskByID(taskID);
  const newTitle = prompt("Enter new title:", currentTask.title);
  const newDescription = prompt("Enter new description:", currentTask.description);
  if (newTitle !== null && newDescription !== null) {
    newTask = await updateTaskDB(taskID, newTitle, newDescription, statuses.indexOf(currentTask.status), currentTask.taskPriority, currentTask.storyPoints);
    showBacklog();
  }
}
//

///////////////////////////////////////////////////////////////////
// Function to display the Sprint management screen
document.getElementById("sprintButton").addEventListener("click", () => {
  showSprint();
});

async function showSprint() {
  const screen = document.getElementById("screen");
  screen.innerHTML = "";

  const projectSprints = await getSprintsByProject(selectedID);

  // Create a table for sprints
  const table = document.createElement("table");
  table.style.width = "100%";
  table.style.borderCollapse = "collapse";

  // Table header
  const header = document.createElement("tr");
  ["Sprint ID", "Title", "Description", "Start Date", "End Date", "Actions"].forEach(text => {
    const th = document.createElement("th");
    th.textContent = text;
    th.style.border = "1px solid #ddd";
    th.style.padding = "8px";
    th.style.textAlign = "left";
    th.style.backgroundColor = "#f2f2f2";
    header.appendChild(th);
  });
  table.appendChild(header);

  // Table rows for sprints
  projectSprints.forEach(sprint => {
    const row = document.createElement("tr");

    // Sprint details
    ["id", "title", "description", "startDate", "endDate"].forEach(key => {
      const cell = document.createElement("td");
      cell.textContent = sprint[key];
      cell.style.border = "1px solid #ddd";
      cell.style.padding = "8px";
      row.appendChild(cell);
    });

    // Actions
    const actionsCell = document.createElement("td");
    actionsCell.style.border = "1px solid #ddd";
    actionsCell.style.padding = "8px";

    // Update button
    const updateButton = document.createElement("button");
    updateButton.textContent = "Update";
    updateButton.onclick = () => updateSprint(sprint.id);
    updateButton.setAttribute("id", "updateButton");
    actionsCell.appendChild(updateButton);

    // Delete button
    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Delete";
    deleteButton.style.marginLeft = "10px";
    deleteButton.onclick = () => deleteSprint(sprint.id);
    deleteButton.setAttribute("id", "deleteButton");
    actionsCell.appendChild(deleteButton);

    const commentsButton = document.createElement("button");
    commentsButton.textContent = "Comments";
    commentsButton.style.marginLeft = "10px";
    commentsButton.onclick = () => showComments("Sprint", sprint.id);
    commentsButton.setAttribute("id", "commentsButton")
    actionsCell.appendChild(commentsButton);

    // Manage taskItems button
    const manageTasksButton = document.createElement("button");
    manageTasksButton.textContent = "Manage Tasks";
    manageTasksButton.style.marginLeft = "10px";
    manageTasksButton.onclick = () => manageSprintTasks(sprint.id);
    manageTasksButton.setAttribute("id", "updateButton");
    actionsCell.appendChild(manageTasksButton);

    row.appendChild(actionsCell);
    table.appendChild(row);
  });

  screen.appendChild(table);

  // Add Sprint form
  const form = document.createElement("div");
  form.style.marginTop = "20px";

  const titleInput = document.createElement("input");
  titleInput.type = "text";
  titleInput.placeholder = "Sprint Title";
  titleInput.style.marginRight = "10px";

  const descriptionInput = document.createElement("input");
  descriptionInput.type = "text";
  descriptionInput.placeholder = "Sprint Description";
  descriptionInput.style.marginRight = "10px";

  const startDateInput = document.createElement("input");
  startDateInput.type = "date";
  startDateInput.style.marginRight = "10px";

  const endDateInput = document.createElement("input");
  endDateInput.type = "date";
  endDateInput.style.marginRight = "10px";

  const addButton = document.createElement("button");
  addButton.textContent = "Add Sprint";
  addButton.onclick = () =>
    addSprint(titleInput.value, descriptionInput.value, startDateInput.value, endDateInput.value);
  addButton.setAttribute("id", "addButton");

  form.appendChild(titleInput);
  form.appendChild(descriptionInput);
  form.appendChild(startDateInput);
  form.appendChild(endDateInput);
  form.appendChild(addButton);

  screen.appendChild(form);
}

// Function to add a new sprint
async function addSprint(title, description, startDate, endDate) {
  if (!title || !description || !startDate || !endDate) {
    alert("All fields are required.");
    return;
  }
  await createSprintDB(selectedID, title, description, startDate, endDate);
  showSprint();
}

// Function to delete a sprint
async function deleteSprint(sprintID) {
  await deleteSprintDB(sprintID);
  showSprint();
}

// Function to update a sprint
async function updateSprint(sprintID) {
  const sprint = await getSprintByID(sprintID);
  const newTitle = prompt("Enter new title:", sprint.title);
  const newDescription = prompt("Enter new description:", sprint.description);
  const newStartDate = prompt("Enter new start date:", sprint.startDate);
  const newEndDate = prompt("Enter new end date:", sprint.endDate);

  if (newTitle !== null && newDescription !== null && newStartDate !== null && newEndDate !== null) {
    await updateSprintDB(sprintID, newTitle, newDescription, newStartDate, newEndDate);
    showSprint();
  }
}

//Task managment in sprints
async function manageSprintTasks(sprintID) {
  const screen = document.getElementById("screen");
  screen.innerHTML = "";

  const users = await getProjectTeam(selectedID);
  const sprint = await getSprintByID(sprintID);
  const allTasks = await getTasksByProject(selectedID);
  const sprintTasks = allTasks.filter(task => task.sprintId === sprintID);
  const availableTasks = allTasks.filter(task => !task.sprintId);

  const header = document.createElement("h2");
  header.textContent = `Manage Tasks for Sprint: ${sprint.title}`;
  screen.appendChild(header);

  // Table for taskItems in the sprint
  const table = document.createElement("table");
  table.style.width = "100%";
  table.style.borderCollapse = "collapse";

  // Table header
  const headerRow = document.createElement("tr");
  ["Title", "Priority", "Status", "Story Points", "Team Members", "Actions"].forEach(text => {
    const th = document.createElement("th");
    th.textContent = text;
    th.style.border = "1px solid #ddd";
    th.style.padding = "8px";
    th.style.backgroundColor = "#f2f2f2";
    headerRow.appendChild(th);
  });
  table.appendChild(headerRow);

  // Table rows for each task
  sprintTasks.forEach(task => {
    const row = document.createElement("tr");

    // Title
    const titleCell = document.createElement("td");
    titleCell.textContent = task.title;
    row.appendChild(titleCell);

    // Priority
    const priorityCell = document.createElement("td");
    priorityCell.textContent = task.priority;
    row.appendChild(priorityCell);

    // Status
    const statusCell = document.createElement("td");
    statusCell.textContent = task.status;
    row.appendChild(statusCell);

    // Story Points
    const storyPointsCell = document.createElement("td");
    storyPointsCell.textContent = task.storyPoints;
    row.appendChild(storyPointsCell);

    // Team Members
    const teamCell = document.createElement("td");
    const assignedMembers = task.assignedUsers?.[0]?.fullName ?? "Unknown User";
    teamCell.textContent = assignedMembers;
    row.appendChild(teamCell);

    // Actions
    const actionsCell = document.createElement("td");
    const removeButton = document.createElement("button");
    removeButton.textContent = "Remove Task";
    removeButton.onclick = () => {
      unAssignTaskFromSprintDB(task.id).then(response => {
        removeUserFromTask(task.assignedUsers[0].id, task.id).then(response =>{
          manageSprintTasks(sprintID);
        });
      });

    };
    removeButton.setAttribute("id", "deleteButton");
    actionsCell.appendChild(removeButton);
    row.appendChild(actionsCell);

    table.appendChild(row);
  });

  screen.appendChild(table);

  // Add Task to Sprint Form
  const form = document.createElement("div");
  form.style.marginTop = "20px";

  const taskSelect = document.createElement("select");
  taskSelect.style.marginRight = "10px";
  availableTasks.forEach(task => {
    const option = document.createElement("option");
    option.value = task.id;
    option.textContent = task.title;
    taskSelect.appendChild(option);
  });
  form.appendChild(taskSelect);

  const prioritySelect = document.createElement("select");
  prioritySelect.style.marginRight = "10px";
  ["Low", "Medium", "High", "VeryHigh"].forEach(priority => {
    const option = document.createElement("option");
    option.value = priority;
    option.textContent = priority;
    prioritySelect.appendChild(option);
  });
  form.appendChild(prioritySelect);

  const statusSelect = document.createElement("select");
  statusSelect.style.marginRight = "10px";
  ["OnHold", "Current", "Done", "Testing"].forEach(status => {
    const option = document.createElement("option");
    option.value = status;
    option.textContent = status;
    statusSelect.appendChild(option);
  });
  form.appendChild(statusSelect);

  const storyPointsInput = document.createElement("input");
  storyPointsInput.type = "number";
  storyPointsInput.placeholder = "Story Points";
  storyPointsInput.style.marginRight = "10px";
  form.appendChild(storyPointsInput);

  const teamSelect = document.createElement("select");
  teamSelect.multiple = true;
  teamSelect.style.marginRight = "10px";
  const teamMembers = users;

  teamMembers.forEach(member => {
    const option = document.createElement("option");
    option.value = member.id;
    option.textContent = member.fullName;
    teamSelect.appendChild(option);
  });
  form.appendChild(teamSelect);

  const addButton = document.createElement("button");
  addButton.textContent = "Add Task";
  addButton.onclick = () => {
    console.log("selected user ID: ", teamSelect.value);
    addTaskToSprint(taskSelect.value, sprintID, statusSelect.value, prioritySelect.value, storyPointsInput.value, teamSelect.value);
  };
  addButton.setAttribute("id", "addButton");

  form.appendChild(addButton);
  screen.appendChild(form);
}
//

async function addTaskToSprint(IDofTask, sprintID, status, priority, storyPoints, userID) {
  const taskID = parseInt(IDofTask);
  await assignTaskToSprintDB(taskID, sprintID);
  await attachUserToTask(userID, IDofTask);
  const updTask = await getTaskByID(taskID);

  const priorities = ["Low", "Medium", "High", "VeryHigh"];
  const statuses = ["OnHold", "Current", "Done", "Testing"];

  const priorityIndex = priorities.indexOf(priority);
  const statusIndex = statuses.indexOf(status);

  await updateTaskDB(taskID, updTask.title, updTask.description, statusIndex, priorityIndex, storyPoints);

  manageSprintTasks(sprintID); // Refresh the view
}

///////////////////////////////////////////////////////////////////
// Function to display the Board
document.getElementById("boardButton").addEventListener("click", () => {
  displayBoard();
});

let expandedSprintID = null; // Track which sprint is expanded

async function displayBoard() {
  const screen = document.getElementById("screen");
  screen.innerHTML = "";

  const header = document.createElement("h2");
  header.textContent = "Board: Sprints and Task Statuses";
  screen.appendChild(header);

  const sprints = await getSprintsByProject(selectedID);
  sprints.forEach((sprint) => {
    // Sprint Row
    const sprintDiv = document.createElement("div");
    sprintDiv.setAttribute("id", "sprint-container");

    const headerDiv = document.createElement("div");
    headerDiv.setAttribute("id", "header-container");

    const sprintHeader = document.createElement("h3");
    sprintHeader.textContent = `${sprint.title} (Start: ${sprint.startDate}, End: ${sprint.endDate})`;
    headerDiv.appendChild(sprintHeader);

    sprintDiv.appendChild(headerDiv);

    const toggleButton = document.createElement("button");
    toggleButton.textContent = expandedSprintID === sprint.id ? "Hide Tasks" : "Show Tasks";
    toggleButton.onclick = () => {
      expandedSprintID = expandedSprintID === sprint.id ? null : sprint.id;
      displayBoard();
    };
    toggleButton.setAttribute("id", "updateButton");
    sprintHeader.appendChild(toggleButton);

    // Show tasks if this sprint is expanded
    console.log(expandedSprintID === sprint.id);
    if (expandedSprintID === sprint.id) {
      const tasksContainer = document.createElement("div");
      tasksContainer.setAttribute("id", "section-container");

      // Create sections for statuses as columns
      const statuses = ["OnHold", "Current", "Done", "Testing"];
      statuses.forEach((status) => {
        var appendAtTask = false;

        const section = document.createElement("div");

        const sectionLabel = document.createElement("h4");
        sectionLabel.textContent = status;
        section.appendChild(sectionLabel);
        
        section.ondragover = (e) => e.preventDefault(); // Allow drop
        section.ondrop = (e) => {
          const taskID = e.dataTransfer.getData("text/plain");
          getTaskByID(taskID).then(task => {
            if (task) {
              const priorities = ["Low", "Medium", "High", "VeryHigh"];
              console.log("Updating status", task.status);
              console.log("To", statuses.indexOf(status));
              console.log("Or", status);
              updateTaskDB(task.id, task.title, task.description, statuses.indexOf(status), priorities.indexOf(task.priority), task.storyPoints).then(response => {
                displayBoard(); // Refresh the board
              }); 
            }
          });
        };
        getTasksByProject(selectedID).then(tasksInStatus => {
          const chosenTasks = tasksInStatus.filter(
            (task) => task.sprintId === sprint.id && task.status === status
          );
          console.log("chosen tasks", chosenTasks);
          chosenTasks.forEach((task) => {
            const taskDiv = document.createElement("div");
            taskDiv.draggable = true;
            taskDiv.ondragstart = (e) => e.dataTransfer.setData("text/plain", task.id);
            taskDiv.setAttribute("id", "task-card");

            // Get team members for the task
            getUserByID(task.assignedUsers[0].id).then(teamMembers => {
              // Task details
              const taskDetails = `
            <strong>ID:</strong> ${task.id} <br>
            <strong>Name:</strong> ${task.title} <br>
            <strong>Priority:</strong> ${task.priority} <br>
            <strong>Story Points:</strong> ${task.storyPoints} <br>
            <strong>Team Members:</strong> ${task.assignedUsers?.[0]?.fullName ?? "Unknown User"}
          `;
              taskDiv.innerHTML = taskDetails;
              section.appendChild(taskDiv);
              appendAtTask = true;
              tasksContainer.appendChild(section);
            });
          });
        });
        if(!appendAtTask) tasksContainer.appendChild(section);
      });

      sprintDiv.appendChild(tasksContainer);
    }

    screen.appendChild(sprintDiv);
  });
}

async function getTaskUsingID(taskID) {
  console.log("Trying to get task:", taskID);
  return await getTaskByID(taskID);
}

async function getUserUsingID(userID) {
  const name = await getUserByID(userID);
  return name.fullName;
}

//Login
let currentUser = null; // To store the logged-in user

async function handleLogin(event) {
  event.preventDefault(); // Prevent form submission

  const email = document.getElementById('email').value;
  const password = document.getElementById('password').value;
  const errorMessage = document.getElementById('error-message');

  // Check if the user exists
  const users = await getAllUsers();
  user = users.find(u => u.email === email/* && u.password === password*/);

  if (user) {
    currentUser = user;
    document.getElementById('login-popup').style.display = 'none';
    document.getElementById('content').style.display = 'block';

    // Optionally, display user info
    console.log("Logged in as:", user.fullName);
  } else {
    errorMessage.textContent = 'Invalid email or password.';
  }
}

// Show login popup on page load
window.onload = function () {
  document.getElementById('login-popup').style.display = 'flex';
};

///////////////////////////
//Comments
async function showComments(entityType, entityID) {
  const screen = document.getElementById("screen");
  screen.innerHTML = "";

  const header = document.createElement("h2");
  header.textContent = `Comments for ${entityType} ID: ${entityID}`;
  screen.appendChild(header);

  // Filter comments for the current entity
  const entityComments = await getCommentsByEntity(entityType, entityID);

  // Comments Container
  const commentsContainer = document.createElement("div");
  commentsContainer.setAttribute("id", "comments-container");
  commentsContainer.style.border = "1px solid #ddd";
  commentsContainer.style.padding = "10px";
  commentsContainer.style.marginBottom = "20px";
  commentsContainer.style.backgroundColor = "#f9f9f9";

  entityComments.forEach((comment) => {
    const commentDiv = document.createElement("div");
    commentDiv.setAttribute("id", "comment-div");
    commentDiv.style.borderBottom = "1px solid #ccc";
    commentDiv.style.padding = "10px";


    getUserUsingID(comment.userId).then(userName => {
      console.log(`Fetching comment with userId: ${comment.userId}`);
      console.log(`Users name: ${userName}`);

      commentDiv.innerHTML = `
      <div>
        <strong>User:</strong> ${userName ? userName : "Unknown"}<br>
        <strong>Created:</strong> ${comment.created}
      </div>
      <div><p>${comment.content}</p></div>
    `;
    });
    commentsContainer.appendChild(commentDiv);
  });

  if (entityComments.length === 0) {
    commentsContainer.innerHTML = "<p>No comments yet.</p>";
  }

  screen.appendChild(commentsContainer);

  // Add New Comment Section
  const newCommentSection = document.createElement("div");
  newCommentSection.style.border = "1px solid #ddd";
  newCommentSection.style.padding = "10px";
  newCommentSection.style.backgroundColor = "#f4f4f4";

  const newCommentLabel = document.createElement("h3");
  newCommentLabel.textContent = "Add a Comment:";
  newCommentSection.appendChild(newCommentLabel);

  const commentInput = document.createElement("textarea");
  commentInput.style.width = "100%";
  commentInput.style.height = "100px";
  commentInput.placeholder = "Write your comment here...";
  newCommentSection.appendChild(commentInput);

  const addButton = document.createElement("button");
  addButton.textContent = "Add Comment";
  addButton.style.marginTop = "10px";
  addButton.style.padding = "10px";
  addButton.style.backgroundColor = "#007BFF";
  addButton.style.color = "white";
  addButton.style.border = "none";
  addButton.style.cursor = "pointer";

  addButton.onclick = () => {
    console.log(`User: ${currentUser.id} is adding a comment`);
    createComment(entityType, entityID, currentUser.id, commentInput.value.trim())
  };

  newCommentSection.appendChild(addButton);
  screen.appendChild(newCommentSection);
}

async function createComment(entityType, entityID, userID, commentInput) {
  const newComment = commentInput;
  if (newComment === "") {
    alert("Comment cannot be empty!");
    return;
  }

  await addComment(entityType, entityID, userID, newComment);
  showComments(entityType, entityID); // Refresh comments view
}