/*
 * Copyright © 2012-2015 VMware, Inc.  All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the “License”); you may not
 * use this file except in compliance with the License.  You may obtain a copy
 * of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an “AS IS” BASIS, without
 * warranties or conditions of any kind, EITHER EXPRESS OR IMPLIED.  See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */


using System;
using AppKit;
using System.Collections.Generic;
using Foundation;
using Vmware.Tools.RestSsoAdminSnapIn.Nodes;
using Vmware.Tools.RestSsoAdminSnapIn.Dto;
using System.Security.Cryptography.X509Certificates;
using Vmware.Tools.RestSsoAdminSnapIn.Core.Extensions;
using Vmware.Tools.RestSsoAdminSnapIn.Helpers;

namespace Vmware.Tools.RestSsoAdminSnapIn.DataSource
{
	public class GroupsDataSource : NSTableViewDataSource
	{
		public List<GroupDto> Entries { get; set; }

		public GroupsDataSource ()
		{
			Entries = new List<GroupDto> ();
		}

		// This method will be called by the NSTableView control to learn the number of rows to display.
		[Export ("numberOfRowsInTableView:")]
		public int NumberOfRowsInTableView (NSTableView table)
		{
			if (Entries != null)
				return Entries.Count;
			else
				return 0;
		}

		// This method will be called by the control for each column and each row.
		[Export ("tableView:objectValueForTableColumn:row:")]
		public NSObject ObjectValueForTableColumn (NSTableView table, NSTableColumn col, int row)
		{
			var value = (NSString)string.Empty;
			ActionHelper.Execute (delegate() {
				if (Entries != null) {
					var obj = (this.Entries [row]) as GroupDto;

					switch (col.Identifier) {
					case "Name":
						value = (NSString)obj.GroupName;
						break;
					case "Domain": 
						value = (NSString)obj.GroupDomain;
						break;
					case "Description": 
						value = (NSString)obj.GroupDetails.Description;
						break;
					default:
						break;
					}
				}
			});
			return value;
		}

	}
}

