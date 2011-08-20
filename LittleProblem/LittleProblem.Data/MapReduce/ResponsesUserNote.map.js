mapUserNote = function () {
    this.Responses.forEach(function (res) {
        var member = db.Members.findOne(res.UserId);
        member.Note = res.Note;
        emit(res.UserId, member);
    });
}