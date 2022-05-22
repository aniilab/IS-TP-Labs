const uri = 'api/Artists';
let artists = [];

function getArtists() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayArtists(data))
        .catch(error => console.error('Unable to get artists.', error));
}

function addArtist() {
    const addNameTextbox = document.getElementById('add-name');
    const addCountryTextbox = document.getElementById('add-country');

    const artist = {
        name: addNameTextbox.value.trim(),
        country: addCountryTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(artist)
    })
        .then(response => response.json())
        .then(() => {
            getArtists();
            addNameTextbox.value = '';
            addCountryTextbox.value = '';
        })
        .catch(error => console.error('Unable to add artist.', error));
}

function deleteArtist(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getArtists())
        .catch(error => console.error('Unable to delete artist.', error));
}

function displayEditForm(id) {
    const artist = artists.find(artist => artist.id === id);

    document.getElementById('edit-id').value = artist.id;
    document.getElementById('edit-name').value = artist.name;
    document.getElementById('edit-country').value = artist.country;
    document.getElementById('editForm').style.display = 'block';
}

function updateArtist() {
    const artistId = document.getElementById('edit-id').value;
    const artist = {
        id: parseInt(artistId, 10),
        name: document.getElementById('edit-name').value.trim(),
        country: document.getElementById('edit-country').value.trim()
    };

    fetch(`${uri}/${artistId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(artist)
    })
        .then(() => getArtists())
        .catch(error => console.error('Unable to update artist.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}


function _displayArtists(data) {
    const tBody = document.getElementById('artists');
    tBody.innerHTML = '';


    const button = document.createElement('button');

    data.forEach(artist => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${artist.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteArtist(${artist.id})`);

        let tr = tBody.insertRow();


        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(artist.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(artist.country);
        td2.appendChild(textNodeInfo);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    artists = data;
}
