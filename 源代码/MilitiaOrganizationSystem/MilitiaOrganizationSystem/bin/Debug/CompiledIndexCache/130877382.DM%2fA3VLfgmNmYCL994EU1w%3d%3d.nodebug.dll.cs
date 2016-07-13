using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;

public class Index_Militias_All : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Militias_All()
	{
		this.ViewText = @"from militia in docs.Militias
select new {
	Group = militia.Group,
	Place = militia.Place,
	Name = militia.Name,
	Sex = militia.Sex,
	Source = militia.Source,
	Ethnic = militia.Ethnic,
	Resvalueence = militia.Resvalueence,
	PoliticalStatus = militia.PoliticalStatus,
	CredentialNumber = militia.CredentialNumber,
	Education = militia.Education,
	MilitaryForceType = militia.MilitaryForceType,
	MilitaryRank = militia.MilitaryRank,
	Available = militia.Available,
	EquipmentInfo_Available = militia.EquipmentInfo_Available,
	EquipmentInfo_MilitaryProfessionTypeName = militia.EquipmentInfo_MilitaryProfessionTypeName,
	RetirementRank = militia.RetirementRank,
	RetirementMilitaryForceType = militia.RetirementMilitaryForceType,
	CadreServiced = militia.CadreServiced,
	CadreProfessionalTrained = militia.CadreProfessionalTrained,
	CadreAttendedCommittee = militia.CadreAttendedCommittee,
	CadreTrained = militia.CadreTrained,
	CadreFullTime = militia.CadreFullTime,
	Trained = militia.Trained,
	TeamingPosition = militia.TeamingPosition,
	ReplyStatus = militia.ReplyStatus,
	MilitaryProfessionTypeName = militia.MilitaryProfessionTypeName,
	RetirementProfessionType = militia.RetirementProfessionType,
	MilitaryProfessionName = militia.MilitaryProfessionName,
	RetirementProfessionSmallType = militia.RetirementProfessionSmallType,
	RetirementProfessionName = militia.RetirementProfessionName
}";
		this.ForEntityNames.Add("Militias");
		this.AddMapDefinition(docs => 
			from militia in ((IEnumerable<dynamic>)docs)
			where string.Equals(militia["@metadata"]["Raven-Entity-Name"], "Militias", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				Group = militia.Group,
				Place = militia.Place,
				Name = militia.Name,
				Sex = militia.Sex,
				Source = militia.Source,
				Ethnic = militia.Ethnic,
				Resvalueence = militia.Resvalueence,
				PoliticalStatus = militia.PoliticalStatus,
				CredentialNumber = militia.CredentialNumber,
				Education = militia.Education,
				MilitaryForceType = militia.MilitaryForceType,
				MilitaryRank = militia.MilitaryRank,
				Available = militia.Available,
				EquipmentInfo_Available = militia.EquipmentInfo_Available,
				EquipmentInfo_MilitaryProfessionTypeName = militia.EquipmentInfo_MilitaryProfessionTypeName,
				RetirementRank = militia.RetirementRank,
				RetirementMilitaryForceType = militia.RetirementMilitaryForceType,
				CadreServiced = militia.CadreServiced,
				CadreProfessionalTrained = militia.CadreProfessionalTrained,
				CadreAttendedCommittee = militia.CadreAttendedCommittee,
				CadreTrained = militia.CadreTrained,
				CadreFullTime = militia.CadreFullTime,
				Trained = militia.Trained,
				TeamingPosition = militia.TeamingPosition,
				ReplyStatus = militia.ReplyStatus,
				MilitaryProfessionTypeName = militia.MilitaryProfessionTypeName,
				RetirementProfessionType = militia.RetirementProfessionType,
				MilitaryProfessionName = militia.MilitaryProfessionName,
				RetirementProfessionSmallType = militia.RetirementProfessionSmallType,
				RetirementProfessionName = militia.RetirementProfessionName,
				__document_id = militia.__document_id
			});
		this.AddField("Group");
		this.AddField("Place");
		this.AddField("Name");
		this.AddField("Sex");
		this.AddField("Source");
		this.AddField("Ethnic");
		this.AddField("Resvalueence");
		this.AddField("PoliticalStatus");
		this.AddField("CredentialNumber");
		this.AddField("Education");
		this.AddField("MilitaryForceType");
		this.AddField("MilitaryRank");
		this.AddField("Available");
		this.AddField("EquipmentInfo_Available");
		this.AddField("EquipmentInfo_MilitaryProfessionTypeName");
		this.AddField("RetirementRank");
		this.AddField("RetirementMilitaryForceType");
		this.AddField("CadreServiced");
		this.AddField("CadreProfessionalTrained");
		this.AddField("CadreAttendedCommittee");
		this.AddField("CadreTrained");
		this.AddField("CadreFullTime");
		this.AddField("Trained");
		this.AddField("TeamingPosition");
		this.AddField("ReplyStatus");
		this.AddField("MilitaryProfessionTypeName");
		this.AddField("RetirementProfessionType");
		this.AddField("MilitaryProfessionName");
		this.AddField("RetirementProfessionSmallType");
		this.AddField("RetirementProfessionName");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("Group");
		this.AddQueryParameterForMap("Place");
		this.AddQueryParameterForMap("Name");
		this.AddQueryParameterForMap("Sex");
		this.AddQueryParameterForMap("Source");
		this.AddQueryParameterForMap("Ethnic");
		this.AddQueryParameterForMap("Resvalueence");
		this.AddQueryParameterForMap("PoliticalStatus");
		this.AddQueryParameterForMap("CredentialNumber");
		this.AddQueryParameterForMap("Education");
		this.AddQueryParameterForMap("MilitaryForceType");
		this.AddQueryParameterForMap("MilitaryRank");
		this.AddQueryParameterForMap("Available");
		this.AddQueryParameterForMap("EquipmentInfo_Available");
		this.AddQueryParameterForMap("EquipmentInfo_MilitaryProfessionTypeName");
		this.AddQueryParameterForMap("RetirementRank");
		this.AddQueryParameterForMap("RetirementMilitaryForceType");
		this.AddQueryParameterForMap("CadreServiced");
		this.AddQueryParameterForMap("CadreProfessionalTrained");
		this.AddQueryParameterForMap("CadreAttendedCommittee");
		this.AddQueryParameterForMap("CadreTrained");
		this.AddQueryParameterForMap("CadreFullTime");
		this.AddQueryParameterForMap("Trained");
		this.AddQueryParameterForMap("TeamingPosition");
		this.AddQueryParameterForMap("ReplyStatus");
		this.AddQueryParameterForMap("MilitaryProfessionTypeName");
		this.AddQueryParameterForMap("RetirementProfessionType");
		this.AddQueryParameterForMap("MilitaryProfessionName");
		this.AddQueryParameterForMap("RetirementProfessionSmallType");
		this.AddQueryParameterForMap("RetirementProfessionName");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("Group");
		this.AddQueryParameterForReduce("Place");
		this.AddQueryParameterForReduce("Name");
		this.AddQueryParameterForReduce("Sex");
		this.AddQueryParameterForReduce("Source");
		this.AddQueryParameterForReduce("Ethnic");
		this.AddQueryParameterForReduce("Resvalueence");
		this.AddQueryParameterForReduce("PoliticalStatus");
		this.AddQueryParameterForReduce("CredentialNumber");
		this.AddQueryParameterForReduce("Education");
		this.AddQueryParameterForReduce("MilitaryForceType");
		this.AddQueryParameterForReduce("MilitaryRank");
		this.AddQueryParameterForReduce("Available");
		this.AddQueryParameterForReduce("EquipmentInfo_Available");
		this.AddQueryParameterForReduce("EquipmentInfo_MilitaryProfessionTypeName");
		this.AddQueryParameterForReduce("RetirementRank");
		this.AddQueryParameterForReduce("RetirementMilitaryForceType");
		this.AddQueryParameterForReduce("CadreServiced");
		this.AddQueryParameterForReduce("CadreProfessionalTrained");
		this.AddQueryParameterForReduce("CadreAttendedCommittee");
		this.AddQueryParameterForReduce("CadreTrained");
		this.AddQueryParameterForReduce("CadreFullTime");
		this.AddQueryParameterForReduce("Trained");
		this.AddQueryParameterForReduce("TeamingPosition");
		this.AddQueryParameterForReduce("ReplyStatus");
		this.AddQueryParameterForReduce("MilitaryProfessionTypeName");
		this.AddQueryParameterForReduce("RetirementProfessionType");
		this.AddQueryParameterForReduce("MilitaryProfessionName");
		this.AddQueryParameterForReduce("RetirementProfessionSmallType");
		this.AddQueryParameterForReduce("RetirementProfessionName");
		this.AddQueryParameterForReduce("__document_id");
	}
}
