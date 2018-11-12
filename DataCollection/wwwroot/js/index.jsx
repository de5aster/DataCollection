var { Button, Col, Form, FormGroup, FormControl, HelpBlock, ControlLabel, Nav, NavItem, Table, Tooltip, OverlayTrigger  } = ReactBootstrap;

var works = [];
var equipments = [];
var apiLoad = "home/api/load";
var apiSave = "home/api/save";
var apiSaveDatabase = "home/api/savedatabase";
var apiGetAll = "home/api/getall";
var apiGetExcel = "home/api/download";
var apiGetContractCount = "home/api/getcontractcount";

class MasterWork extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            value: ""            
        };
    }

    onChange = (e) => {
        var val = e.target.value;
        this.setState({
            value: val
        });
    }
    onChangeValue = () => {
        if ( this.state.value  != null)
        {
           setTimeout(this.props.updateMasterWork(this.state.value, this.props.number), 50);
        }
    }

    render() {
        return (
            <FormGroup validationState={this.props.validation}>
                <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}>Выполненные работы* :</Col>                
                <Col sm={6}>
                    <FormControl id="data-label"
                        type="text"
                        value={this.state.value}
                        onChange={this.onChange}
                        onBlur={this.onChangeValue}
                    ></FormControl>
                </Col>
            </FormGroup>
            );
    }
}

class RepairEquipment extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: "",
            count: ""
        };
    }
    onChangeNameValue = (e) => {
        this.setState({
            name: e.target.value
        });
    }

    onChangeCountValue = (e) => {
        this.setState({
            count: e.target.value
        });
    }
    onChangeValue = () => {
        setTimeout(this.props.updateRepairEquipment(this.state.name, this.state.count, this.props.number), 50);
    }

    render() {
        return (
            <FormGroup>
                <Col sm={7}>
                    <FormControl id="data-label"
                        type="text"
                        value={this.state.name}
                        onChange={this.onChangeNameValue}
                        onBlur={this.onChangeValue}></FormControl>
                </Col>
                <Col sm={3}>
                    <FormControl id="input-count"
                        type="number"
                        value={this.state.count}
                        onChange={this.onChangeCountValue}
                        onBlur={this.onChangeValue}></FormControl>
                </Col>
            </FormGroup>
        );
    }
}

class HomePage extends React.Component
{
    constructor(props) {
        super(props);
        this.state = {
            addVisible: false,
            loadVisible: false,
            databaseVisible: false,
            activeNav: "0",
            contractCount: "",
            deserializeFile: [{
                contractId: "",
                clientName: "",
                clientAddress: "",
                phoneNumber: "",
                email: "",
                equipment: "",
                breakage: "",
                masterName: "",
                masterPersonnelNumber: "",
                putDate: "",
                performDate: "",
                works: [],
                repairEquipments: []
            }]
        };
    }
    onAddClick = (e) => {
        e.preventDefault();
        this.setState(()=> ({
            addVisible: true,
            loadVisible: false,
            databaseVisible: false
        }));
        this.onGetContractCount();
    }
    onLoadClick = (e) => {
        e.preventDefault();
        this.setState(() => ({
            addVisible: false,
            loadVisible: true,
            databaseVisible: false
        }));
    }
    onDatabaseClick = (e) => {
        e.preventDefault();
        this.setState(() => ({
            addVisible: false,
            loadVisible: false,
            databaseVisible: true
        }));
        this.onGetDatabase();
    }
    navItemSelect = (eventKey, e) => {
        e.preventDefault();
        this.setState({
            activeNav: eventKey
        });
    }

    onGetContractCount = () => {
        fetch(apiGetContractCount)
            .then((res) => {
                if (res.status === 200) {
                    return res.json();
                }
                if (res.status === 400) {
                    this.setState({
                        error: "400"
                    });
                }
                return null;
            }, function () {
                this.setState({
                    error: "Что-то пошло не так. Попробуйте обновить страницу и повторить попытку"

                });
            }).then((data) => {
                this.setState({
                    contractCount: data + 1
                });
            });
    }

    onGetDatabase = () => {
        fetch(apiGetAll)
            .then((res) => {
                    if (res.status === 200) {
                        return res.json();
                    }
                    if (res.status === 400) {
                        this.setState({
                            error: "400"
                        });
                    }
                    return null;
            },
            function () {
                this.setState({
                    error: "Что-то пошло не так. Попробуйте обновить страницу и повторить попытку"

                });
            }).then((data) => {
                this.setState({
                    deserializeFile: data
                });
            });
    }
    render() {
        return (
            <div>
                <h1>Карта технических работ</h1>
                <Nav bsStyle="tabs" activeKey={this.state.activeNav} onSelect={this.navItemSelect}>
                    <NavItem eventKey="1" onClick={this.onAddClick}>
                        Создать новую карту
                    </NavItem>
                    <NavItem eventKey="2" onClick={this.onLoadClick}>
                        Загрузить созданную карту
                    </NavItem>
                    <NavItem eventKey="3" onClick={this.onDatabaseClick}>
                        База данных
                    </NavItem>
                </Nav>
                <BtnGroup
                    apiUrlLoad={apiLoad}
                    addVisible={this.state.addVisible}
                    loadVisible = {this.state.loadVisible}
                />
                <DataCollection
                    addVisible={this.state.addVisible}
                    apiUrlSave={apiSave}
                    apiUrlSaveDatabase={apiSaveDatabase}
                    contractCount={this.state.contractCount}
                />
                <DatabasePage
                    databaseVisible={this.state.databaseVisible}
                    deserializeFile={this.state.deserializeFile}
                    apiUrlGetExcel={apiGetExcel}
                />
            </div>
            );
    }
}

class DataCollection extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            clientNameValidationState: "",
            clientAddressValidationState: "",
            phoneNumberValidationState: "",
            emailValidationState: "",
            equipmentValidationState: "",
            breackageValidationState: "",
            masterNameValidationState: "",
            masterPersonnelNumberValidationState: "",
            putDateValidationState: "",
            performDateValidationState: "",
            workListValidationState: "",
            numEquipment: 0,
            numWorkList: 0,
            ContractId: 0,
            ClientName: "",
            ClientAddress: "",
            PhoneNumber: "",
            Email: "",
            Equipment: "",
            Breakage: "",
            MasterName: "",
            MasterPersonnelNumber: "",
            PutDate: "",
            PerformDate: "",
            WorkList: [],
            RepairEquipments: [],
            inputList:[],
            materialVisible: false,
            workButtonText: "Добавить работу",
            materialButtonText: "Добавить материал"
        };
    }

    onChangeClientName = (e) => {

        this.setState({
            ClientName: e.target.value,
            clientNameValidationState: ""
        });
    }
    onChangeClientAdress = (e) => {
        this.setState({
            ClientAddress: e.target.value,
            clientAddressValidationState: ""
        });
    }
    onChangePhoneNumber = (e) => {
        this.setState({
            PhoneNumber: e.target.value,
            phoneNumberValidationState: ""
        });
    }
    onChangeEmail = (e) => {
        this.setState({
            Email: e.target.value,
            emailValidationState: ""
        });
    }
    onChangeEquipment = (e) => {
        this.setState({
            Equipment: e.target.value,
            equipmentValidationState: ""
        });
    }
    onChangeBreakage = (e) => {
        this.setState({
            Breakage: e.target.value,
            breackageValidationState: ""
        });
    }
    onChangePutDate = (e) => {
        this.setState({
            PutDate: e.target.value,
            putDateValidationState: ""
        });
    }
    onChangeMasterName = (e) => {
        this.setState({
            MasterName: e.target.value,
            masterNameValidationState:""
        });
    }
    onChangeMasterPersonnelNumber = (e) => {
        this.setState({
            MasterPersonnelNumber: e.target.value,
            masterPersonnelNumberValidationState:""
        });
    }
    onChangePerformDate = (e) => {
        this.setState({
            PerformDate: e.target.value,
            performDateValidationState:""
        });
    } 

    updateMasterWork = (value, number) => {
        if (value != null)
        {
            works[number] = value;
            this.setState({
                WorkList: works,
                workListValidationState: ""
            });
        }
    }

    updateRepairEquipment = (name, count, number) => {
        if (name != null && count != null)
        {
            equipments[number] = [name, count];
            this.setState({
                RepairEquipments : equipments
            });
        }
    }
    onDatabaseSaveClick = () => {
        this.onCheckValid();
        var data = JSON.stringify({
            "ContractId": this.props.contractCount,
            "ClientName": this.state.ClientName,
            "ClientAddress": this.state.ClientAddress,
            "PhoneNumber": this.state.PhoneNumber,
            "Email": this.state.Email,
            "Equipment": this.state.Equipment,
            "Breakage": this.state.Breakage,
            "MasterName": this.state.MasterName,
            "MasterPersonnelNumber": this.state.MasterPersonnelNumber,
            "PutDate": this.state.PutDate,
            "PerformDate": this.state.PerformDate,
            "WorkList": this.state.WorkList,
            "RepairEquipments": this.state.RepairEquipments
        });
        var xhr = new XMLHttpRequest();
        xhr.open("post", this.props.apiUrlSaveDatabase, true);
        xhr.setRequestHeader("Content-type", "application/json");
        xhr.onload = function() {
            if (xhr.status === 200) {
                alert(xhr.responseText);
            }
            if (xhr.status === 406) {
                alert("Форма заполнена некорректно");
            }
            if (xhr.status === 208) {
                alert("Карточка с таким номером заказа уже существует. Обновите страницу.");
            }
        }
        xhr.send(data); 
    }

    onSaveClick = () => {
        this.onCheckValid();
        var data = JSON.stringify({
            "ContractId": this.props.contractCount,
            "ClientName": this.state.ClientName,
            "ClientAddress": this.state.ClientAddress,
            "PhoneNumber": this.state.PhoneNumber,
            "Email": this.state.Email,
            "Equipment": this.state.Equipment,
            "Breakage": this.state.Breakage,
            "MasterName": this.state.MasterName,
            "MasterPersonnelNumber": this.state.MasterPersonnelNumber,
            "PutDate": this.state.PutDate,
            "PerformDate": this.state.PerformDate,
            "WorkList": this.state.WorkList,
            "RepairEquipments": this.state.RepairEquipments
        });
        var xhr = new XMLHttpRequest();
        xhr.open("post", this.props.apiUrlSave, true);
        xhr.setRequestHeader("Content-type", "application/json");
        xhr.onload = function () {
            if (xhr.status === 200) {
                var blob = xhr.response;
                this.saveOrOpenBlob(blob);
            }
            if (xhr.status === 406) {
                alert("Форма заполнена некорректно");
            }
        }.bind(this);
        xhr.send(data);
    }  
 
    saveOrOpenBlob = (blob) => {
        var data = new Blob([blob], { type: "text/xml" }),        
            fileUrl = window.URL.createObjectURL(data),
            tempLink = document.createElement("a");
        tempLink.href = fileUrl;
        tempLink.setAttribute("download", "client.xml");
        tempLink.click();
    }

    onAddMasterWork = () => {
        this.setState({
            numWorkList: this.state.numWorkList + 1,
            workButtonText: "Добавить ещё"
        });
    }

    onDeleteMasterWork = () => {
        if (this.state.numWorkList > 1)
        {
            var index = this.state.numWorkList - 1;
            var works = this.state.WorkList; 
            works.splice(index, 1);
        } else
        {
            var index = 0;
            var works = [];
        }
        this.setState({
            numWorkList: index,
            WorkList: works
        })
    }
    onAddRepairEquipments = () => {
        this.setState({
            numEquipment: this.state.numEquipment + 1,
            materialVisible: true,
            materialButtonText: "Добавить ещё"
        });
    }

    onDeleteRepairEquipments = () => {
        if (this.state.numEquipment > 1) {
            var index = this.state.numEquipment - 1;
            var equips = this.state.RepairEquipments;
            var visible = true;
            equips.splice(index, 1);
        } else
        {
            var index = 0;
            var equips = [];
            var visible = false;
        }
        this.setState({
            numEquipment: index,
            RepairEquipments: equips,
            materialVisible: visible
        })
    }
    onClearClick = (e) => {
        e.preventDefault();
        this.setState({
            numEquipment: 0,
            numWorkList: 0,
            ContractId: 0,
            ClientName: "",
            ClientAddress: "",
            PhoneNumber: "",
            Email: "",
            Equipment: "",
            Breakage: "",
            MasterName: "",
            MasterPersonnelNumber: "",
            PutDate: "",
            PerformDate: "",
            WorkList: [],
            RepairEquipments: [],
            inputList: [],
            materialVisible: false,
            workButtonText: "Добавить работу",
            materialButtonText: "Добавить материал"
        });
    }
    onCheckValid = () => {
        let clientNameValid = "";
        let clientAddressValid = "";
        let phoneNumberValid = "";
        let emailValid = "";
        let equipmentValid = "";
        let breackageValid = "";
        let masterNameValid = "";
        let masterPersonnelNumberValid = "";
        let putDateValid = "";
        let performDateValid = "";
        let worksValid = "";
        if (this.state.ClientName.length === 0) {
            clientNameValid = "error";           
        }
        if (this.state.ClientAddress.length === 0) {
            clientAddressValid = "error";
        }
        if (this.state.PhoneNumber.length === 0) {
            phoneNumberValid = "error";
        }
        if (this.state.Email.length === 0) {
            emailValid = "error";
        }
        if (this.state.Equipment.length === 0) {
            equipmentValid = "error";
        }
        if (this.state.Breakage.length === 0) {
            breackageValid = "error";
        }
        if (this.state.PutDate.length != 10) {
            putDateValid = "error";
        }
        if (this.state.PerformDate.length != 10)
        {
            performDateValid = "error";
        }
        if (this.state.MasterName.length === 0)
        {
            masterNameValid = "error";
        }
        if (this.state.MasterPersonnelNumber.length === 0) {
            masterPersonnelNumberValid = "error";
        }
        if (this.state.WorkList.length === 0) {
            worksValid = "error";
        }
        this.setState({
            clientNameValidationState: clientNameValid,
            clientAddressValidationState: clientAddressValid,
            phoneNumberValidationState: phoneNumberValid, 
            emailValidationState: emailValid,
            equipmentValidationState: equipmentValid,
            breackageValidationState: breackageValid,
            masterNameValidationState: masterNameValid,
            masterPersonnelNumberValidationState: masterPersonnelNumberValid,
            putDateValidationState: putDateValid,
            performDateValidationState: performDateValid,
            workListValidationState: worksValid
        })
    }
    render() {
        const workList = [];
        for (var i = 0; i < this.state.numWorkList; i += 1) {
            workList.push(<MasterWork key={i} number={i} updateMasterWork={this.updateMasterWork} validation={this.state.workListValidationState}/>);
        }
        const equipmentList = [];
        for (var i = 0; i < this.state.numEquipment; i += 1) {
            equipmentList.push(<RepairEquipment key={i} number={i} updateRepairEquipment={this.updateRepairEquipment}/>);
        }
        const tooltip = (
            <Tooltip id="tooltip">
                <strong>Формируется автоматически</strong>
            </Tooltip>
        );
        return (
            <div className={`visible${this.props.addVisible ? "" : "_none"}`} style={{ paddingLeft: "30px" }}>
                <div style={{maxWidth: "600px"}}>
                    <h3>Информация о заказчике</h3>
                    <p> * отмечены обязательные поля</p>
                    <Form horizontal>
                        <FormGroup>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left", paddingTop: "0px" }}>Номер заказа : </Col>  
                            <Col sm={6}>
                        <OverlayTrigger placement="right" overlay={tooltip}>
                            <label style = {{paddingRight:"5px"}}>{this.props.contractCount}</label>
                        </OverlayTrigger>  
                            </Col>
                        </FormGroup>
                        <FormGroup validationState={this.state.clientNameValidationState}>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}>ФИО заказчика* : </Col>
                            <Col sm={6}>
                                <FormControl id="data-label"
                                    type="text"
                                    value={this.state.ClientName}
                                    placeholder = "Введите значение"
                                    onChange={this.onChangeClientName}></FormControl>
                            </Col>
                        </FormGroup>
                        <FormGroup validationState={this.state.clientAddressValidationState}>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}>Адрес проживания* : </Col>
                            <Col sm={6}>
                                <FormControl id="data-label"
                                        type = "text"
                                        value={this.state.ClientAddress}
                                        placeholder="Введите значение"
                                        onChange={this.onChangeClientAdress}></FormControl>
                            </Col>
                        </FormGroup>
                        <FormGroup validationState={this.state.phoneNumberValidationState}>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}>Контактный телефон* : </Col>
                            <Col sm={6}>
                                <FormControl id="data-label"
                                        type="tel"
                                        value={this.state.PhoneNumber}
                                        placeholder="Введите значение"
                                        onChange={this.onChangePhoneNumber}></FormControl>
                            </Col>
                        </FormGroup>
                        <FormGroup validationState={this.state.emailValidationState}>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}>Почта: </Col>
                            <Col sm={6}>
                                <FormControl id="data-label"
                                        type="email"
                                        value = {this.state.Email}
                                        placeholder="Введите значение"
                                        onChange={this.onChangeEmail}></FormControl>
                            </Col>
                        </FormGroup>
                        <FormGroup validationState={this.state.equipmentValidationState}>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}>Оборудование* : </Col>
                            <Col sm={6}>
                                <FormControl id="data-label"
                                        type="text"
                                        value={this.state.Equipment}
                                        placeholder="Введите значение"
                                        onChange={this.onChangeEquipment}></FormControl>
                            </Col>
                        </FormGroup>
                        <FormGroup validationState={this.state.breackageValidationState}>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}>Причина сдачи* : </Col>
                            <Col sm={6}> <FormControl id="data-label"
                                    type="text"
                                    value={this.state.Breakage}
                                    placeholder="Введите значение"
                                    onChange={this.onChangeBreakage}></FormControl>
                            </Col>
                        </FormGroup>
                        <FormGroup validationState={this.state.putDateValidationState}>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}>Дата сдачи* : </Col>
                            <Col sm={6}>
                                <FormControl id="date-label"
                                    type="date"
                                    value={this.state.PutDate}
                                    onChange={this.onChangePutDate}></FormControl>
                            </Col>
                        </FormGroup>
                    </Form>
                    <h3>Информация об исполнителе</h3>
                    <Form horizontal>
                        <FormGroup validationState={this.state.masterNameValidationState}>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}> ФИО Мастера* : </Col>
                            <Col sm={6}>
                                <FormControl id="data-label"
                                    type="text"
                                    value={this.state.MasterName}
                                    placeholder="Введите значение"
                                    onChange={this.onChangeMasterName}></FormControl>
                            </Col>
                        </FormGroup>
                        <FormGroup validationState={this.state.masterPersonnelNumberValidationState}>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}>Табельный номер* : </Col>
                            <Col sm={6}>
                                <FormControl id="data-label"
                                type="text"
                                value={this.state.MasterPersonnelNumber}
                                placeholder="Введите значение"
                                onChange={this.onChangeMasterPersonnelNumber}></FormControl>
                            </Col>
                        </FormGroup>
                        <FormGroup validationState={this.state.performDateValidationState}>
                            <Col componentClass={ControlLabel} sm={4} style={{ textAlign: "left" }}>Дата выполения* : </Col>
                            <Col sm={6}>
                                <FormControl id="date-label"
                                    type="date"
                                    value={this.state.PerformDate}
                                    onChange={this.onChangePerformDate}></FormControl>
                            </Col>
                        </FormGroup>
                    </Form>

                    <h3 className={this.state.workListValidationState}>Выполненные работы*</h3>
                    <Form horizontal>                        
                        {workList}
                    </Form>
                    <Button onClick={this.onAddMasterWork} style={{ marginRight: "5px" }}>{this.state.workButtonText}</Button>
                    <Button onClick={this.onDeleteMasterWork}>Удалить</Button>
                    <h3>Расходные материалы</h3>
                    <div className={`visible-material${this.state.materialVisible ? "" : "-none"}`}>
                        <Form horizontal>
                            <FormGroup>
                                <Col componentClass={ControlLabel} sm={7} style={{ textAlign: "left" }}>Материал</Col>
                                <Col componentClass={ControlLabel} sm={3} style={{ textAlign: "left" }}>Количество </Col>
                            </FormGroup>                        
                            {equipmentList}
                        </Form>
                    </div>
                    <Button onClick={this.onAddRepairEquipments} style={{marginRight:"5px"}}>{this.state.materialButtonText}</Button>
                    <Button onClick={this.onDeleteRepairEquipments}>Удалить</Button>
                    <br />
                    <br />
                    <Button style={{ marginRight: "5px", marginTop: "5px" }} onClick={this.onDatabaseSaveClick}>Сохранить</Button>
                    <Button style={{ marginRight: "5px", marginTop: "5px"  }} onClick={this.onSaveClick}>Сохранить в файл</Button>
                    <Button onClick={this.onClearClick}>Очистить форму</Button>
                    
                </div>
            </div>
            );
    }
}

class BtnGroup extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            invisible: this.props.loadVisible,
            deserializeFile: {
                contractId: "",
                clientName: "",
                clientAddress: "",
                phoneNumber: "",
                email: "",
                equipment: "",
                breakage: "",
                masterName: "",
                masterPersonnelNumber: "",
                putDate: "",
                performDate: "",
                works:[],
                repairEquipments: []
            }
        };
    }
    onChangeFile = (e) => {

        this.setState({
            file: e.target.files[0],
            name: e.target.files[0].name
        });
        
    }
    onClickLoad = (e) => {
        e.preventDefault();
        var formData = new FormData();
        formData.append("file", this.state.file);
        fetch(this.props.apiUrlLoad, {
            mode: "no-cors",
            method: "POST",
            body: formData
        }).then((res) => {
            if (res.status === 200) {
                return res.json();
            }
            if (res.status === 411) {
                alert("Пустой файл. Выберите корректный файл");
            }
            if (res.status === 409) {
                alert("Некорректный файл. Выберите корректный файл");
            }
        }, function () {
            this.setState({
                error: "Что-то пошло не так. Попробуйте обновить страницу и повторить попытку"

            });
        }).then((data) => {
            if (data != null) {
                this.setState({
                    deserializeFile: data,
                    invisible: true
                });
            }
        });
    }  

    render() {
        return (
            <div>
                <form className={`visible${this.props.loadVisible ? "" : "_none"}`} onSubmit={this.onClickLoad} enctype="multipart/form-data" style={{ marginBottom: "10px" }}>
                    <FormGroup>
                        <FormControl
                            type="file"
                            id="file"
                            name="file"
                            accept=".xml"
                            onChange={this.onChangeFile} />
                    </FormGroup>
                    <Button type="submit" disabled={!this.state.file}>Загрузить</Button>
                    <div className={`visible${this.state.invisible ? "" : "_none"}`}>
                        <h3>Информация о заказчике</h3>
                        <table>
                            <tbody>
                            <tr>
                                <td id="tbl-lbl"><label>Номер заказа: </label></td>
                                <td>{this.state.deserializeFile.contractId}</td>
                            </tr>
                                <tr>
                                    <td id="tbl-lbl"><label>ФИО Заказчика: </label></td>
                                    <td>{this.state.deserializeFile.clientName}</td>
                                </tr>
                                <tr>
                                    <td> <label>Адрес проживания: </label></td>
                                    <td> {this.state.deserializeFile.clientAddress}</td>
                                </tr>
                                <tr>
                                    <td><label>Контактный телефон: </label></td>
                                    <td>{this.state.deserializeFile.phoneNumber}</td>
                                </tr>
                                <tr>
                                    <td><label>Почта: </label></td>
                                    <td>{this.state.deserializeFile.email}</td>
                                </tr>
                                <tr>
                                    <td> <label>Оборудование: </label></td>
                                    <td>{this.state.deserializeFile.equipment}</td>
                                </tr>
                                <tr>
                                    <td><label>Причина сдачи: </label></td>
                                    <td>{this.state.deserializeFile.breakage}</td>
                                </tr>
                                <tr>
                                    <td><label>Дата сдачи: </label></td>
                                    <td>{this.state.deserializeFile.putDate.slice(0,10)}</td>
                                </tr>
                            </tbody>
                        </table>
                        <h3>Информация об исполнителе</h3>
                        <table>
                            <tbody>
                                <tr>
                                    <td id="tbl-lbl"><label> ФИО застера: </label></td>
                                    <td>{this.state.deserializeFile.masterName}</td>
                                </tr>
                                <tr>
                                    <td> <label>Табельный номер: </label></td>
                                    <td>{this.state.deserializeFile.masterPersonnelNumber}</td>
                                </tr>
                                <tr>
                                    <td><label>Дата выполения: </label></td>
                                    <td>{this.state.deserializeFile.performDate.slice(0,10)}</td>
                                </tr>
                            </tbody>
                        </table>
                        <h3>Выполненные работы: </h3>
                        <table>
                            <tbody>
                        {
                            this.state.deserializeFile.works.map((item,index) => {
                                return (
                                    <tr>
                                        <td style={{paddingRight: "15px"}}>{index +1}.</td>
                                        <td>{item.name}</td>
                                    </tr>
                                )})
                        }
                            </tbody>
                        </table>
                        <h3>Материалы:</h3>
                        <table className="material-table">
                            <thead>
                                <tr>
                                    <td id="number">№</td>
                                    <td>Материал</td>
                                    <td>Количество</td>
                                </tr>
                            </thead>
                            <tbody>
                            {
                                this.state.deserializeFile.repairEquipments.map((item, index) => {
                                    return (
                                    
                                        <tr id="item">
                                            <td id="number">{index + 1}. </td>
                                            <td>{item.name}</td>
                                            <td>{item.count} шт.</td>
                                        </tr>
                                    );
                                })}
                            </tbody>
                        </table>
                    </div>
                </form>
            </div>
            );
    }

}

class DatabasePage extends React.Component
{
    constructor(props) {
        super(props);
        this.state = {
        };
    }

    onGetExcel = (e) => {
        e.preventDefault();
        fetch(this.props.apiUrlGetExcel)
            .then(function(response) {
                return response.blob();
            })
            .then(function (xlsxBlob) {
                var a = document.createElement("a");
                document.body.appendChild(a);
                a.style = "display: none";
                let url = window.URL.createObjectURL(xlsxBlob);
                a.href = url;
                a.setAttribute("download", "clients.xlsx");
                a.click();
            });
    };

    saveOrOpenBlob = (blob) => {
        var data = new Blob([blob], {
            type: "application/otcet-stream"
        }),
            fileUrl = window.URL.createObjectURL(data),
            tempLink = document.createElement("a");
        tempLink.href = fileUrl;
        tempLink.click();
    }
    render() {
            return (               
            <div className={`visible${this.props.databaseVisible ? "" : "_none"}`}>
                <Button onClick={this.onGetExcel}>Скачать базу в Excel</Button>
                <br />
                <br />
                <Table striped bordered condensed hover>
                    <thead>
                    <tr>
                        <th>Номер заказа</th>
                        <th>Имя Клиента</th>
                        <th>Адрес клиента</th>
                        <th>Телефон</th>
                        <th>Почта</th>
                        <th>Оборудование</th>
                        <th>Причина сдачи</th>
                        <th>Дата сдачи</th>
                        <th>Мастер</th>
                        <th>Табельный номер</th>
                        <th>Дата выполнения</th>
                        <th>Выполненные работы</th>
                        <th>Расходные материалы</th>
                    </tr>
                    </thead>
                    <tbody>
                    {
                        this.props.deserializeFile.map( function(item) {
                            return (
                                <tr>
                                    <td>{item.contractId}</td>
                                    <td>{item.clientName}</td>
                                    <td>{item.clientAddress}</td>
                                    <td>{item.phoneNumber}</td>
                                    <td>{item.email}</td>
                                    <td>{item.equipment}</td>
                                    <td>{item.breakage}</td>
                                    <td style={{minWidth:"85px"}}>{item.putDate.slice(0, 10)}</td>
                                    <td>{item.masterName}</td>
                                    <td>{item.masterPersonnelNumber}</td>
                                    <td style={{ minWidth: "85px" }}>{item.performDate.slice(0, 10)}</td>
                                    <td style = {{wordWrap:"normal"}}>{item.works}</td>
                                    <td style = {{ wordWrap: "normal" }}>{item.repairEquipments}</td>
                                </tr>
                            );
                        })
                        }
                    </tbody>
                </Table>
            </div>
        );
    }
}

ReactDOM.render(
    <HomePage />,
    document.getElementById("content")
);