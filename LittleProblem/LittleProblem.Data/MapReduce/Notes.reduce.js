reduceNotes = function (key, vals) {
    var note = 0;
    var member = vals[0];

    for (var n = 0; n < vals.length; n++) {
        note += vals[n].Note;
    }

    member.Note = note;
    return member;
}