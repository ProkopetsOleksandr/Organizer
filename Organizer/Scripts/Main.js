function MakeDraggable() {

    const list_items = document.querySelectorAll('.list-item');
    const lists = document.querySelectorAll('.list');

    let draggedItem = null;

    for (let i = 0; i < list_items.length; i++) {
        const item = list_items[i];

        //Init delete button
        $(item).on('click', '.js-delete', function () {
            var button = $(this);
            $.ajax({
                url: "/api/goal/" + button.attr('data-goalId'),
                method: "DELETE",
                success: function () {
                    button.parent().css('display', 'none');
                }
            });
        });




        item.addEventListener('dragstart', function () {
        draggedItem = item;

            setTimeout(function () {
        item.style.display = 'none';
            }, 0);

        });

        item.addEventListener('dragend', function () {
        setTimeout(function () {
            draggedItem.style.display = 'block';


            //let liText = draggedItem.innerText;
            let goalId = parseInt(draggedItem.dataset.goalid);


            let div = draggedItem.parentElement.parentElement;

            if (div.classList.contains("category-list")) {
                let p = div.getElementsByTagName('p')[0];
                let projectId = parseInt(p.dataset.projectid);

                //Set goal(goalId) to project(projectId)
                $.ajax({
                    url: "/api/planner/move-planner/",
                    data: { goalId: goalId, projectId: projectId },
                    method: "POST"
                });
            }
            else {
                //Set goal(goalId) to 
                $.ajax({
                    url: "/api/planner/move-planner/",
                    data: { goalId: goalId, projectId: -1 },
                    method: "POST"
                });
            }

            draggedItem = null;
        }, 0);
        });

        for (let j = 0; j < lists.length; j++) {
            const list = lists[j];

            list.addEventListener('dragover', function (e) {
        e.preventDefault();
            });

            list.addEventListener('dragenter', function (e) {
        e.preventDefault();
                this.style.backgroudColor = 'rgba(0, 0, 0, 0.2)';
            });

            list.addEventListener('dragleave', function (e) {
        e.preventDefault();
                this.style.backgroudColor = 'rgba(0, 0, 0, 0.1)';
            });

            list.addEventListener('drop', function (e) {
        this.append(draggedItem);
                this.style.backgroudColor = 'rgba(0, 0, 0, 0.1)';
            });
        }
    }
}




function InitializeInputs() {
    const inputs = document.querySelectorAll('.add-input');

    for (let i = 0; i < inputs.length; i++) {
        const item = inputs[i];

        item.addEventListener('keyup', function (event) {
            if (event.key === "Enter") {
        event.preventDefault();
                let div = item.parentElement;
                let p = div.getElementsByTagName('p')[0];

                let plannerId = parseInt($('#planner-id').val());
                let projectId = parseInt(p.dataset.projectid);
                let name = item.value;

                $.ajax({
                    url: "/api/planner/",
                    data: {plannerId: plannerId, projectId: projectId, goalName: name },
                    method: "POST",
                    success: function (goal) {

                        var li = $("<li class='list-item' draggable='true' data-goalId=" + goal.Id + ">");
                        var span = $("<span class='goal-name'>")
                        var content = document.createTextNode(goal.Name);

                        let checkbox = $("<input type='checkbox' class='goal-check' data-goalId=" + goal.Id + ">");
                        let deleteBtn = $("<button class='btn btn-danger badge pull-right js-delete' data-goalId=" + goal.Id + ">Delete</Button>")

                        li.css("border-left", "3px solid #fb2401");


                        
                        li.append(checkbox);
                        span.append(content)
                        li.append(span);
                        li.append(deleteBtn);

                        let ul = div.getElementsByTagName('ul')[0];
                        $(ul).append(li);
                    }
                });
                            
                item.value = "";
            }
        });
    }
}


function InitializeCheckboxes() {
    const checkboxes = document.querySelectorAll('.goal-check');
    for (var i = 0; i < checkboxes.length; i++) {

        const item = checkboxes[i];

        item.addEventListener('change', function (event) {

            let goalId = parseInt(item.dataset.goalid);

            $.ajax({
                url: "/api/goal",
                data: { id: goalId },
                method: "GET",
                success: function () {

                    var parent = item.parentElement;

                    if (event.target.checked == true) {
                        parent.style.borderLeft = "3px solid green";
                    }
                    else {
                        parent.style.borderLeft = "3px solid #fb2401";
                    }
                }
            });
        })

    }
}



//$(document).ready(function () {

//    $('.list-item').on('click', '.js-delete', function () {

//        var button = $(this);
//        $.ajax({
//            url: "/api/goals/" + button.attr('data-goalId'),
//            method: "DELETE",
//            success: function () {
//                button.parent().css('display', 'none');
//            }
//        });

//    });

//});