window.stampaValutazioni = function (jsonValutazioni) {
    // Parsa il JSON ricevuto
    const valutazioni = JSON.parse(jsonValutazioni);

    // Crea una nuova finestra o un elemento per la stampa
    let printWindow = window.open('', '_blank');
    printWindow.document.write('<html><head><title>Stampa Valutazioni</title></head><body>');
    printWindow.document.write('<h1>Valutazioni Studente</h1>');
    printWindow.document.write('<table border="1"><thead><tr><th>Studente</th><th>Materia</th><th>Tipo</th><th>Voto</th><th>Data</th><th>Commento</th></tr></thead><tbody>');

    // Aggiungi una riga per ogni valutazione
    valutazioni.forEach(function (valutazione) {
        printWindow.document.write('<tr>');
        printWindow.document.write(`<td>${valutazione.Studente}</td>`);
        printWindow.document.write(`<td>${valutazione.Materia}</td>`);
        printWindow.document.write(`<td>${valutazione.Tipo}</td>`);
        printWindow.document.write(`<td>${valutazione.Voto}</td>`);
        printWindow.document.write(`<td>${valutazione.Data}</td>`);
        printWindow.document.write(`<td>${valutazione.Commento}</td>`);
        printWindow.document.write('</tr>');
    });

    printWindow.document.write('</tbody></table>');
    printWindow.document.write('</body></html>');
    printWindow.document.close();

    // Chiama la funzione di stampa
    printWindow.print();
};
