﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBO
{
    public static class StoredProcedure
    {
        public const string Login = "LOGIN";
        public const string CreateEmployee = "CreateEmployee";
        public const string GET_ALL_USERS = "GET_ALL_USERS";
        public const string DeleteEmployee = "DeleteEmployee";
        public const string CHANGE_PASSWORD = "CHANGE_PASSWORD";
        public const string UPDATE_EMPLOYEE = "UPDATE_EMPLOYEE";
        public const string GETALL_PERIOD = "GETALL_PERIOD";
        public const string GET_PERIOD = "GET_PERIOD";
        public const string INSERT_MBO_RESULT = "INSERT_MBO_RESULT";
        public const string GET_MBO_RESULTID = "GET_MBO_RESULTID";
        public const string INSERT_MBO_PLAN = "INSERT_MBO_PLAN";
        public const string DELETE_MBO_PLAN = "DELETE_MBO_PLAN";
        public const string CONFIRM_MBO_RESULT = "CONFIRM_MBO_RESULT";
        public const string UPDATE_RESULT = "UPDATE_RESULT";
        public const string GET_RESULT_BY_PERIOD = "GET_RESULT_BY_PERIOD";
        public const string GET_RESULT_BY_PERIOD_EMP = "GET_RESULT_BY_PERIOD_EMP";
        public const string GET_WORKGROUP = "GET_WORKGROUP";
        public const string GET_USERS_PAGING = "GET_USERS_PAGING";
        public const string UPDATE_USER = "UPDATE_USER";
        public const string ASSIGN_APPROVER = "ASSIGN_APPROVER";
        public const string INSERT_PERIOD = "INSERT_PERIOD";
        public const string DELETE_PERIOD = "DELETE_PERIOD";
        public const string GET_COUNT_USERS = "GET_COUNT_USERS";
        public const string GET_APPROVER_BY_EMP = "GET_APPROVER_BY_EMP";
        public const string GET_PLAN_BY_EMP_PERIOD = "GET_PLAN_BY_EMP_PERIOD";
        public const string GET_ACTION_PLAN_BY_ID = "GET_ACTION_PLAN_BY_ID";
        public const string GET_USER_BY_ID = "GET_USER_BY_ID";
        public const string GET_PLAN_BY_ROLE = "GET_PLAN_BY_ROLE";
        public const string GET_PLAN_DETAILS = "GET_PLAN_DETAILS";
        public const string GET_ROLE = "GET_ROLE";
        public const string GET_RESULT_SELF = "GET_RESULT_SELF";
        public const string GET_ELEMENT_TABLE = "GET_ELEMENT_TABLE";
        public const string UPDATE_PLAN = "UPDATE_PLAN";
        public const string CHECK_ROLE = "CHECK_ROLE";
        public const string UPDATE_RESULT_M1 = "UPDATE_RESULT_M1";
        public const string UPDATE_RESULT_SELF = "UPDATE_RESULT_SELF";
        public const string UPDATE_PLAN_SELF = "UPDATE_PLAN_SELF";
        public const string UPDATE_PLAN_M1 = "UPDATE_PLAN_M1";
        public const string GET_EMPID_BY_RESULT = "GET_EMPID_BY_RESULT";
        public const string INSERT_UPDATE_SCORE = "INSERT_UPDATE_SCORE";
        public const string SEARCH_USERS_PAGING = "SEARCH_USERS_PAGING";
        public const string GET_SEARCH_USERS_COUNT = "GET_SEARCH_USERS_COUNT";
        public const string RESET_PASSWORD = "RESET_PASSWORD";
        public const string GET_RESULT_HR = "GET_RESULT_HR";
        public const string GET_EVA_GROUP = "GET_EVA_GROUP";
        public const string UPDATE_IMG = "UPDATE_IMG";
        public const string CHANGE_APPROVER = "CHANGE_APPROVER";
        public const string DISABLE_PERIOD = "DISABLE_PERIOD";
        public const string GET_EMP_BY_APPROVER = "GET_EMP_BY_APPROVER";
        public const string GET_RESULT_BY_PERIOD_GROUP = "GET_RESULT_BY_PERIOD_GROUP";
        public const string GET_RESULT_HR_BY_EMP = "GET_RESULT_HR_BY_EMP";
        public const string CONFIRM_MBO_PLAN = "CONFIRM_MBO_PLAN";
        public const string UPDATE_PLAN_REGISTER = "UPDATE_PLAN_REGISTER";
        public const string EXPORT_USERS = "EXPORT_USERS";
        public const string GET_PLAN_BY_PERIOD = "GET_PLAN_BY_PERIOD";
        public const string GET_NO_MBO_EMP = "GET_NO_MBO_EMP";
        public const string CHECK_NO_MBO = "CHECK_NO_MBO";
        public const string INSERT_NO_MBO_RESULT = "INSERT_NO_MBO_RESULT";
        public const string UPDATE_PERIOD_SET_NO_MBO = "UPDATE_PERIOD_SET_NO_MBO";

        public const string INSERT_EVA_RULE = "INSERT_EVA_RULE";
        public const string UPDATE_EVA_RULE = "UPDATE_EVA_RULE";
        public const string DELETE_EVA_RULE = "DELETE_EVA_RULE";
        public const string GET_EVA_RULE = "GET_EVA_RULE";
        public const string GET_EVA_RULE_BY_NUM = "GET_EVA_RULE_BY_NUM";
        public const string GET_EVA_SUMMARY = "GET_EVA_SUMMARY";
        public const string GET_COUNT_GRADE = "GET_COUNT_GRADE";
        public const string GET_ROLE_MAX = "GET_ROLE_MAX";
        public const string GET_RESULT_REVIEW = "GET_RESULT_REVIEW";
        public const string UNASSIGN_APPROVER = "UNASSIGN_APPROVER";
        public const string CHECK_ONE_APPROVER = "CHECK_ONE_APPROVER";
        public const string GET_EVA_INFO = "GET_EVA_INFO";
        public const string GET_COUNT_EVA_INFO = "GET_COUNT_EVA_INFO";
        public const string UPDATE_RESULT_REMARK = "UPDATE_RESULT_REMARK";
        public const string UPDATE_RESULT_HR_FINAL = "UPDATE_RESULT_HR_FINAL";
        public const string GET_FACTOR_ID = "GET_FACTOR_ID";
        public const string GET_ELEMENT_HR = "GET_ELEMENT_HR";
        public const string GET_MBO_RESULT = "GET_MBO_RESULT";
        public const string GET_MBO_RESULT_EXPORT = "GET_MBO_RESULT_EXPORT";
        public const string COUNT_MBO_RESULT = "COUNT_MBO_RESULT";
        public const string GET_PLAN_PAGING = "GET_PLAN_PAGING";
        public const string COUNT_PLAN_PAGING = "COUNT_PLAN_PAGING";
        public const string GET_PLAN_EXPORT = "GET_PLAN_EXPORT";
        public const string CHECK_ASSIGN_APPROVER = "CHECK_ASSIGN_APPROVER";
    }
}